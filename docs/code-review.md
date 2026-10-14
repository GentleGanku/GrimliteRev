---
layout: page
title: "Code Review & Technical Debt"
permalink: /code-review/
---

# Grimlite Rev — Code Review & Technical Debt Report

| | |
|---|---|
| **Date** | 2026-10-11 |
| **Commit reviewed** | `c48ab3b` (`main`) |
| **Scope** | Tracked source only: `app/` (C# WinForms client, 288 `.cs` files, ~37.4k lines) and `game-client/` (AS3 bridge, 17 `.as` files, ~3.2k lines), plus build/config files. Binaries (DLL/EXE/SWF/FLA) were inventoried but not inspected. The git-ignored `(C#)/(AS3) Source Code - Inverse` folders are out of scope. |
| **Depth** | Standard. Hotspot files were read in full; the rest were skimmed and pattern-swept (empty `catch`, `async void`, fire-and-forget tasks, unsafe deserialization, `Process.Start`, `int.Parse`, cross-thread UI access, etc.). The C#↔AS3 `ExternalInterface` contract was cross-checked by name. |
| **Limits** | Static review only. The project can't be built from a clean clone (see TD-01) and no tests exist, so nothing was executed. Findings marked *(likely)* depend on runtime game-client behavior that couldn't be observed. |

Severity: **Critical** = exploitable or data-destroying · **High** = a feature is broken or the bot stops/crashes in normal use · **Medium** = fails under realistic edge cases, or degrades reliability/performance · **Low** = latent, cosmetic, or dead-code defect.

---

## Summary

**54 code-review findings:** 1 Critical · 12 High · 24 Medium · 17 Low.
**18 technical-debt items**, scored and phased in Part 2.

### Top 5 issues

1. **CR-01 (Critical): Loading or pasting a bot can execute arbitrary code.** `.gbot` files, clipboard pastes, and the `Load Bot` command all deserialize with Json.NET `TypeNameHandling.All` and no type allow-list. Bot files are traded between players, so one malicious file is enough to compromise a user's PC.
2. **CR-06 (High): Bot failures are silent, or crash the whole client.** The main loop is an `async` delegate passed to `Task.Factory.StartNew`, so exceptions are never observed. There are also 66 `async void` methods and no global exception handlers. Any bad value in a command (e.g. `int.Parse` in a statement) stops the bot while the UI still says it's running.
3. **CR-41 (High): The drop whitelist rejects whitelisted drops** with the default drop UI. The AS3 `rejectDrop` loop only honors the *first* whitelist entry and never resets its index.
4. **CR-32 (High): A missing or slightly malformed `config.cfg`/`BotClientConfig.cfg` makes the client unusable.** It also silently truncates any value containing `=`.
5. **CR-21 / CR-22 / CR-09 / CR-10 / CR-11 (High): several user-facing features don't work at all.** These are travel hotkeys (crash), the `{ROOM_ID}` packet placeholder, the Drop Delay setting, the *"All in Item List is in inventory"* statement, and the *Equip Set* command.

### Overall assessment

The codebase is a decompiled-and-extended WinForms application. Engine, UI, and Flash bridge are tightly interwoven through static singletons. Most defects trace back to three root causes:

- **No error-handling or concurrency model.** Fire-and-forget tasks, `async void`, polling loops without cancellation, and blanket `catch {}` blocks.
- **Duplicated state.** There are two `Bot` instances, two `Configuration` sources of truth, and UI checkboxes read directly from worker threads.
- **A stringly-typed C#↔AS3 bridge.** It has two `Flash.Call` overload families with different escaping rules, hand-quoted JSON strings, and `'"True"'` booleans.

Fixing CR-01 is urgent regardless of the project's archived status, because existing users can still be harmed by shared bot files.

---

## Part 1 — Code Review

### 1.1 Security

#### CR-01 · Critical · Unsafe polymorphic deserialization of bot files and clipboard
- **Where:** `app/Grimoire.UI/BotManager.cs:44-55` (`_serializerSettings` / `_saveSerializerSettings`), used at `BotManager.cs:1449-1478` (`TryDeserializeLoadBot`, `TryDeserialize`), `BotManager.cs:1861` (Ctrl+V paste), `BotManager.cs:1618` (bot tree). Also `app/Grimoire.Botting.Commands.Misc/CmdLoadBot.cs:38-43` and `app/Grimoire.Botting.Commands.Item/CmdEquipSet.cs:20-24`.
- **What:** `TypeNameHandling.All` with no `SerializationBinder` lets the JSON `$type` field instantiate any type the CLR can resolve, including well-known Json.NET gadget types in the .NET Framework GAC.
- **Failure:** A user downloads a `.gbot` from a forum or Discord, or copies "commands" text and presses Ctrl+V in the command list, and code runs with the user's privileges. `CmdLoadBot` extends this further: a benign-looking bot can chain-load a second file at runtime.
- **Fix:** Add an `ISerializationBinder` that allow-lists `Grimoire.*` command, skill, quest and item types (plus `System.Collections.Generic.List`1` of them) and rejects everything else. Use one shared `JsonSerializerSettings` instance. Existing `.gbot` files stay compatible because their `$type` names don't change. Longer-term see TD-09.

#### CR-02 · Medium · `Process.Start` on untrusted strings
- **Where:** `app/Grimoire.Networking/Proxy.cs:278-280` (`openWebsite` from the SWF), `app/Grimoire.UI/BotManager.cs:8426` (links in a bot's RTF description), `app/Grimoire.Tools/Grabber.cs:667`.
- **What:** Arbitrary strings are passed to the shell. The bot description comes from untrusted `.gbot` files, and the bridge is reachable from any loaded SWF (CR-03).
- **Failure:** A description link of `file:///C:/…/payload.exe` or a UNC path launches a program when clicked.
- **Fix:** Parse with `Uri.TryCreate` and only allow `http`/`https` schemes. Optionally confirm before opening a non-AQ domain.

#### CR-03 · Medium · `Security.allowDomain("*")` exposes the bridge
- **Where:** `game-client/grimoire/Root.as:86`.
- **What:** Any SWF loaded into the client (maps, cutscenes, ads) can script `Root` and therefore call `ExternalInterface` endpoints (`openWebsite`, `chatCommand`, `setGameObject` with arbitrary paths).
- **Fix:** Restrict to the AQ game/CDN domains.

#### CR-04 · Medium · Credentials exposed over the bridge and sent unencoded
- **Where:** `game-client/grimoire/Root.as:66-67, 373-382` (plaintext `username`/`password` statics returned via `Username`/`Password` callbacks); `app/Grimoire.Tools.Buyback/AutoBuyBack.cs:36` (`"uuu=" + Username + "&pps=" + Password`).
- **What:** The password is held in a static and handed to any caller of the bridge. The buy-back login form body isn't URL-encoded.
- **Failure:** A password containing `&`, `+`, `%` or `=` is sent corrupted, so buy-back login fails. See also CR-44 for `"` and `\`.
- **Fix:** Build the form body with `FormUrlEncodedContent`. Drop the `Password` callback and pass credentials only where they're needed.

#### CR-05 · Low · Private-build gate is client-side only
- **Where:** `app/Grimoire/Program.cs:19, 41-60`.
- **What:** Access is granted by scraping a GitFront page title, which can be bypassed by flipping one boolean. The private repository URL hash is committed in public source.
- **Fix:** Remove it, or don't treat it as access control.

### 1.2 Bot engine (`Grimoire.Botting`, commands)

#### CR-06 · High · Bot loop exceptions are unobserved; `async void` exceptions crash the app
- **Where:** `app/Grimoire.Botting/Bot.cs:184` (`Task.Factory.StartNew(Activate, …, LongRunning, …)` with an `async` method yields an unobserved `Task<Task>`); `app/Grimoire.UI/Root.cs:133` (same pattern for `Proxy.ClientExecute`); `async void` at `Bot.cs:145, 187, 308, 344, 378, 393, 421, 444` and 66 places overall. There's no `Application.ThreadException`, `AppDomain.UnhandledException` or `TaskScheduler.UnobservedTaskException` handler anywhere.
- **Failure:** Any exception in a command kills `Activate` silently, but `IsRunning` stays `true` and the UI shows the bot as running. Examples: `int.Parse(instance.Value(Value2))` in any of the 91 statements, `Configuration.Tempvariable[key]` for an undefined variable, an empty command list at `Bot.cs:262`. An exception after an `await` inside an `async void` (event handlers, Flash handlers) goes to the WinForms context and terminates or dialogs the client.
- **Fix:** Use `Task.Run(Activate)`, keep the task, and `try/catch` inside `Activate` with logging to `LogForm` and a clean `Stop()`. Convert `async void` to `async Task` except for true UI event handlers, and wrap those. Register global handlers that log to `LogForm`.

#### CR-07 · High · Command index goes negative after dying on index 0
- **Where:** `app/Grimoire.Botting/Bot.cs:247` (`Index - 1`) and the setter at `Bot.cs:50-53`, which only bounds the upper end.
- **Failure:** With *Restart upon death* off, dying while the first command runs sets `Index = -1`, and `Configuration.Commands[-1]` throws. Per CR-06 the bot silently stops.
- **Fix:** Clamp in the setter (`value < 0 || value >= Count ? 0 : value`), or use `Math.Max(0, Index - 1)`.

#### CR-08 · High · `Kill` ignores Stop/Pause and can leak skill loops
- **Where:** `app/Grimoire.Botting.Commands.Combat/CmdKill.cs:32-44, 53`.
- **What:**
  - The wait for the target to die passes its own prerequisite `() => !_cts.IsCancellationRequested`. That replaces the default prerequisite, which checks `instance.IsRunning`.
  - `_cts` is an instance *field* that's reassigned on every `Execute`, and the fire-and-forget `UseSkills` loop reads the field rather than a captured token.
- **Failure:** Pressing Stop mid-fight keeps the command awaiting and the skill loop casting until the monster dies. When the same `CmdKill` object runs again (looping bots), a previous `UseSkills` loop can see the new, uncancelled CTS and keep running, so skill loops pile up. The pile-up is *likely*; it depends on timing.
- **Fix:** Use a local `CancellationTokenSource` linked to the bot's token, pass the token into `UseSkills`, include `instance.IsRunning` in the prerequisite, and await the skill task before returning.

#### CR-09 · High · *Drop Delay* setting never reaches the running bot
- **Where:** `app/Grimoire.UI/BotManager.cs:7845-7848` writes `Bot.Instance.DropDelay`. The engine that actually runs is `BotManager._activeBotEngine = new Bot()` (`BotManager.cs:37`). `Bot.Instance` (`Bot.cs:32`) is a second, never-started instance. `Configuration.DropDelay` is saved and loaded but never read by the engine.
- **Fix:** Read `Configuration.DropDelay` in `Bot.OnItemDropped`, and delete `Bot.Instance` (see TD-06; `HandlerLoadShop`/`HandlerQuestComplete` also use it).

#### CR-10 · High · *"All in Item List is in inventory"* statement is always true
- **Where:** `app/Grimoire.Botting.Commands.Misc.Statements/CmdListInInv.cs:16` uses `Configuration.Instance`, a lazily created *empty* singleton (`app/Grimoire.Botting/Configuration.cs:8-9`) that is never assigned the loaded bot.
- **Failure:** `Items` is empty, so `TrueForAll` is `true` and the statement never skips. `OptionsManager.cs:289` reads the same dead singleton.
- **Fix:** Use `instance.Configuration.Items`, and remove `Configuration.Instance`.

#### CR-11 · High · *Equip Set* command always fails
- **Where:** `app/Grimoire.Botting.Commands.Item/CmdEquipSet.cs:29`.
- **What:** `JsonConvert.DeserializeObject<SetItem>(Application.StartupPath + ItemName, …)` parses the *path string* as JSON instead of reading the file.
- **Failure:** A `JsonReaderException` is thrown on every execution, and per CR-06 the bot stops. Even if that were fixed, `item` can be null (`item.Id`), `Player.Equip(item.Name)` passes a name where an id is expected, and the equip loop has no retry cap.
- **Fix:** Use `File.ReadAllText`, null-check the item, equip by id, and cap retries.

#### CR-12 · Medium · Duplicate skill-set labels crash bot start
- **Where:** `app/Grimoire.Botting/BotUtilities.cs:140` (`BotData.SkillSet.Add(label.ToUpper(), i)`).
- **Failure:** Two labels that differ only by case, or are identical, throw `ArgumentException` inside `async void OnExecute`, which crashes the client on *Start*.
- **Fix:** Use the indexer (`SkillSet[key] = i`) or validate when the label is added.

#### CR-13 · Medium · Quest/boost checkers overlap and can NRE
- **Where:** `app/Grimoire.Botting/Bot.cs:180` (`CheckQuests()` not awaited, loop keyed on `IsRunning` rather than the token), `Bot.cs:328` (`Quests?.Where(...)?.ToList()` can be null, then `.Count` throws), `Bot.cs:251, 308-320` (`async void CheckBoosts` started on *every* loop iteration).
- **Failure:** Stop followed quickly by Start leaves two quest loops running, and boosts are triggered repeatedly while a previous `CheckBoosts` is still waiting.
- **Fix:** Make both `async Task` methods honoring `_ctsBot.Token`, and run `CheckBoosts` inline (await it) or guard it with a flag.

#### CR-14 · Medium · Unawaited `Task.Delay(70)` when unbanking on start
- **Where:** `app/Grimoire.Botting/Bot.cs:170`.
- **Failure:** The delay is a no-op, so all bank transfers are fired back-to-back and some are rejected by the server's action lock.
- **Fix:** `await Task.Delay(70)`, or better, wait on `World.IsActionAvailable(LockActions.Transfer)`.

#### CR-15 · Medium · Bank-on-stop enumerates live inventory across `await`
- **Where:** `app/Grimoire.Botting/Bot.cs:206-214`.
- **Failure:** The inventory list can change while awaiting (transfer responses), which throws `InvalidOperationException` inside `async void OnCancel` and crashes the client.
- **Fix:** Iterate a snapshot (`.ToList()`).

#### CR-16 · Medium · `OptionsManager` background loops duplicate
- **Where:** `app/Grimoire.Botting/OptionsManager.cs:253` (`ApplySettings()` fire-and-forget loop; `Stop()` + `Start()` within one 250 ms tick, as auto-relogin does at `Bot.cs:232-236`, leaves two loops), `OptionsManager.cs:198-207, 322-330` (each `_saveState = true` starts a new 5-minute `SaveState` loop; the old one only re-checks after 5 minutes).
- **Failure:** Provoke packets are sent twice as often, and save-state whispers multiply after every relog.
- **Fix:** Keep one `Task` plus a CTS per loop, and cancel it in `Stop()`.

#### CR-17 · Medium · Packet-spam delay has no effect
- **Where:** `app/Grimoire.Botting/OptionsManager.cs:316-320`.
- **What:** `SetPacketSpam` sends and *then* awaits `PacketDelay` inside `async void`. The caller (`ApplySettings`, `:364-365`) doesn't wait.
- **Failure:** The effective rate is always the 250 ms options timer, regardless of the configured delay.
- **Fix:** Use a dedicated loop that awaits the delay between sends.

#### CR-18 · Medium · AFK option registers a `null` packet handler
- **Where:** `app/Grimoire.Botting/OptionsManager.cs:160-171, 384-392`. `HandlerAFK1` is never assigned (the static constructor only sets `HandlerHideRoom`).
- **Failure:** Enabling AFK adds `null` to `Proxy._handlersXt`. `Proxy.ProcessMessage` (`Proxy.cs:144`) then throws an NRE in the `Where` predicate for every XT packet; it's swallowed by `catch {}`, so any handler after it, including plugin handlers, never runs. The AFK feature itself does nothing.
- **Fix:** Initialize `HandlerAFK1` (or remove the option) and make `RegisterHandler` reject null.

#### CR-19 · High · Auto-relogin crashes when the saved server is missing or busy
- **Where:** `app/Grimoire.Tools/AutoRelogin.cs:47-50`.
- **What:** `Server.List.Find(...)` can return null (the server was renamed or removed, or the list isn't loaded), giving an NRE. `FindAll(...).ToList()[0]` throws `ArgumentOutOfRangeException` when every server is within 10 of its cap.
- **Failure:** This runs inside `Bot.Activate` (`Bot.cs:233`), so a relog during peak hours silently ends the bot (CR-06).
- **Fix:** Fall back to any online server, use `FirstOrDefault`, and retry with backoff.

#### CR-20 · Medium · The "temporarily kicked" wait never waits
- **Where:** `app/Grimoire.Tools/AutoRelogin.cs:58` passes `null` as the prerequisite, so `WaitUntil` uses its default `IsRunning && Player.IsLoggedIn && Player.IsAlive` (`BotUtilities.cs:49`). The player is logged out at this point, so the loop exits immediately.
- **Failure:** Relogin is attempted while still kicked, wasting the retry.
- **Fix:** Pass `() => !cts.IsCancellationRequested`.

#### CR-21 · High · Travel hotkeys always crash the client
- **Where:** `app/Grimoire.UI/Hotkeys.cs:309-316` calls `cmd.Execute(null)`.
- **Failure:** `CmdJoin.Execute` immediately calls `instance.WaitUntil(...)`, whose default prerequisite dereferences `instance.IsRunning`. That NRE is thrown in `async void`, so the client crashes. (`Travel.cs:234-240` correctly passes `BotManager.Instance.ActiveBotEngine`.)
- **Fix:** Pass `BotManager.Instance.ActiveBotEngine`, and share one `ExecuteTravel` helper (TD-07).

### 1.3 Networking, packet handlers and the Flash bridge

#### CR-22 · High · `{ROOM_ID}` placeholder is never substituted
- **Where:** `app/Grimoire.Networking/Proxy.cs:117-125`. `text` holds the replaced string, but `World.SendPacket(data, …)` sends the original.
- **Failure:** Every packet command, spammer entry or tamperer packet using `{ROOM_ID}` is sent literally and rejected by the server.
- **Fix:** Send `text`, and null-check `data` before calling `Replace`.

#### CR-23 · High · Two `Flash.Call` overload families with different escaping and decoding
- **Where:** `app/Grimoire.Tools/Flash.cs:93-130` (`params object[]`, XML-escaped via `ToFlashXml`, result *not* HTML-decoded, `Convert.ChangeType`) vs `Flash.cs:133-163` (`params string[]`, `BuildRequest` with **no escaping** at `:157`, result HTML-decoded then JSON-deserialized). They also use different ActiveX references: static `flash` at `:122` vs `Root.Instance.Client` at `:169`. Which overload binds depends on the *static* argument types at each call site.
- **Failure:** `Flash.Call("RejectDrop", wlArray)` (`app/Grimoire.Game/DropStack.cs:144`), `GameMessage`, `GamePopup`, `ChangeName` and others bind to the string version. Any item name, message or player name containing `<` or `&` produces malformed XML; the call silently does nothing. For example, drop rejection stops working for a whitelist containing an `&`.
- **Fix:** Keep one implementation that always escapes arguments (`SecurityElement.Escape`) and decodes results consistently, and expose typed wrappers (TD-12).

#### CR-24 · Medium · Async packet handlers escape the dispatcher's `try/catch`
- **Where:** `app/Grimoire.Networking.Handlers/HandlerDropItem.cs:41-70`. After `await Task.Delay(10)`, `ParseLog` runs outside `Proxy.ProcessMessage`'s `try`. `Player.Bank.SavedItems.Find(...).Quantity` (`:67`) and `drop.Name` (`:70`) can be null.
- **Failure:** Picking up a drop that goes to the bank but isn't in the cached bank list throws an NRE on the UI context, which crashes the client.
- **Fix:** Null-check, and make handler `Handle` methods synchronous or `async Task` with internal `try/catch`.

#### CR-25 · Medium · Dispatcher swallows all handler errors and aborts the remaining handlers
- **Where:** `app/Grimoire.Networking/Proxy.cs:137-166`.
- **What:** One `try` wraps the whole loop with an empty `catch`. Handler lists are also mutated by plugins or options while being enumerated (`RegisterHandler`/`UnregisterHandler`, `:79-107`).
- **Fix:** Wrap each handler call in its own `try`, log failures, and enumerate a snapshot (`ToArray()`).

#### CR-26 · Medium · Flash callback parsing is unguarded
- **Where:**
  - `app/Grimoire.Tools/Flash.cs:40-57`: `XElement.Parse`, `int.Parse(text2)`.
  - `Flash.cs:51`: the `modifyServers` JSON is wrapped in `<string>` without XML-escaping.
  - `Flash.cs:248-254`: `int.Parse(null)` when a server field is missing.
  - `app/Grimoire.Networking/Proxy.cs:273`: `int.Parse` on the ping value.
- **Failure:** One unexpected server-list field or progress value breaks the login screen's server list, or throws inside an ActiveX event.
- **Fix:** Use `TryParse` with defaults, escape the return value, and guard the event handler.

#### CR-27 · Medium · Waiters that can never be cancelled
- **Where:** `app/Grimoire.Networking.Handlers/HandlerJSON.cs:25-29` (`HandlerArea`), `HandlerLogin.cs:36-50` (`WaitUntil(..., -1, ...)`). The static `_cts` is never cancelled.
- **Failure:** Each `moveToArea` received while logged out or during a cutscene leaves a 500 ms poll running forever. These accumulate over a long session.
- **Fix:** Use a finite timeout, or cancel on logout.

#### CR-28 · Low · Broken or dead bridge helpers
- **Where:** `app/Grimoire.Tools/Flash.cs:68` (`getGameObjectS`) and `:85` (`callGameFunction0`) have no matching AS3 `addCallback`. `Flash.cs:285` indexes an `ExpandoObject` through `dynamic` (`d[key] = …`), which throws `RuntimeBinderException`; `CallHandler`/`FromFlashXml` are never wired up. `app/Grimoire.Game/Player.cs` `HasTargetByID` passes `new int[]{id}`, which serializes as an array where AS3 expects `int` (it's also unused).
- **Fix:** Delete them, or fix them and add callbacks.

#### CR-29 · Medium · `World.VisibleDropUI` binding can't call a method *(likely)*
- **Where:** `app/Grimoire.Game/World.cs:144` binds `"cDropsUI.inner_menu.isMenuOpen()"`, but AS3 `getGameObject` only walks properties (`game-client/grimoire/Root.as:483-505`). The result is `undefined`, so `null`, so the swallowed exception yields `false`.
- **Failure:** `Proxy.ClientExecute` (`Proxy.cs:239-242`) calls `dropUIOpt(false)` every 100 ms for the whole time a bot with rejection enabled runs.
- **Fix:** Add a dedicated AS3 callback (`IsDropMenuOpen`) or use `callGameFunction`.

### 1.4 Plugins

#### CR-30 · Medium · One non-plugin file stops all plugin loading
- **Where:** `app/Grimoire.UI/PluginManager.cs:203-206` uses `paths.All(LoadPlugins)`, which short-circuits on the first `false`. `app/Grimoire.UI/Root.cs:130` passes `Directory.GetFiles(PluginsPath)` with no `*.dll` filter and throws if `Plugins/` doesn't exist.
- **Failure:** A `readme.txt` or `.pdb` in `Plugins/` sorts before the real DLLs, and every later plugin is skipped. A missing folder breaks `Root_Load`.
- **Fix:** `foreach` over `*.dll`, log failures, and create the folder if it's missing.

#### CR-31 · Medium · Startup plugins are registered twice
- **Where:** `app/Grimoire.Tools.Plugins/GrimoirePlugin.cs:77` adds `this` to `LoadedPlugins`, and `app/Grimoire.UI/PluginManager.cs:154` adds it again.
- **Failure:** Each startup plugin appears twice in the list. Selecting by index shows or unloads the wrong plugin, and *Unload* leaves a stale entry.
- **Fix:** Add only in `GrimoirePlugin.Load`.

### 1.5 Configuration and persistence

#### CR-32 · High · Config loader is fragile and lossy
- **Where:** `app/Grimoire.Botting/Config.cs:39-51` and its copy `app/Grimoire.Botting/BotClientConfig.cs:39-51`.
- **What:**
  - `File.Create(path)` returns an open `FileStream` (with `FileShare.None`) that's never disposed, then `File.ReadLines(path)` opens the same file. `Task.Delay(100)` isn't awaited.
  - `l.Split('=')` → `a[1]`.
  - `ToDictionary` throws on duplicate keys.
- **Failure:** If the `.cfg` is missing, the read fails with `IOException`. A line without `=`, or a duplicate key, throws. Any of these happens inside a static initializer, so you get a `TypeInitializationException` and every later `Config.Instance` access fails. Values containing `=` (Base64 padding, travel strings, URLs) are silently truncated on load and then saved truncated.
- **Fix:** Use `File.WriteAllText(path, "")` (or `using`), `Split(new[]{'='}, 2)`, skip malformed lines, and use last-write-wins for duplicates. Merge the two classes (TD-07).

#### CR-33 · Medium · Plain bot descriptions misdetected as compressed
- **Where:** `app/Grimoire.Tools/DocToRtf.cs:11-24` (`IsBase64Encoded`), used at `app/Grimoire.UI/BotManager.cs:692`.
- **Failure:** Any description that happens to be valid Base64 with a length that's a multiple of 4 (e.g. `"Farm"`, `"test1234"`) is "unzipped". `GZipStream` throws `InvalidDataException`, and `ApplyConfiguration` isn't inside the `try`, so loading the bot fails with an error dialog.
- **Fix:** Prefix compressed descriptions with a marker (e.g. `gz:`), or check the GZip magic bytes `1F 8B` after decoding.

### 1.6 UI

#### CR-34 · Medium · GDI object churn in the owner-drawn command list
- **Where:** `app/Grimoire.UI/BotManager.cs:7941-8103` (`lstCommands_DrawItem`). Four or more `SolidBrush`, a `Font`, and further brushes in the colored-text paths are created on every `DrawItem` and never disposed.
- **Failure:** The selected index changes on every bot step, so the list repaints constantly. GDI handles are only reclaimed by the finalizer, which causes flicker or slowdown over long sessions and risks hitting the 10k GDI handle limit.
- **Fix:** Cache brushes and fonts per color/size, or use `using`.

#### CR-35 · Low · Command edits silently discarded
- **Where:** `app/Grimoire.UI/BotManager.cs:868-886`. `Items[SelectedIndex]` throws when `SelectedIndex == -1`, and an invalid edited JSON is dropped by `catch {}` without telling the user.

#### CR-36 · Low · Unchecked parsing and nulls in click handlers
- **Where:** `app/Grimoire.UI/BotManager.cs:1360` (`int.Parse(numQuestID.Text)`), `app/Grimoire.UI/Hotkeys.cs:320` (`SelectedItem.ToString()` before the `SelectedIndex` check), `app/Grimoire.Tools/ChatCommand.cs:70, 77, 98` (`int.Parse` in `async void` from chat input like `/shop abc`, which crashes).

#### CR-37 · Low · Capture-protection wiring is inconsistent
- **Where:** `app/Grimoire.UI/Root.cs:1742-1762`. The static `MainForms` is built during type initialization, when `Root.Instance` is still null; it also eagerly constructs every singleton form. `Root.cs:1705` hard-codes `enableCapture = true`, and `:1781` stores consent under the unrelated key `"ReloadMap"`.

#### CR-38 · Low · Keyboard hook P/Invoke is 32-bit only
- **Where:** `app/Grimoire.Tools/KeyboardHook.cs:32-43`. Hook handles and `wParam`/`lParam` are declared as `int`. The csproj has x64 configurations, where these truncate pointers.
- **Fix:** Use `IntPtr`.

#### CR-39 · Low · Unbounded growth
- **Where:** `app/Grimoire.Game/World.cs:88` (`LoadedShops` appends on every shop load), `app/Grimoire.Networking/Proxy.cs:263, 269` (packet logger textboxes are never trimmed).

#### CR-40 · Low · Shared drop list and a mutating query
- **Where:** `app/Grimoire.Game/DropStack.cs`. `_drops` is written on the UI thread (handlers) and read from the bot's worker thread. `Contains(string)` *removes* the entry from `Bot.DropsInInventory` as a side effect, so a second identical query returns a different answer.

### 1.7 AS3 game client

#### CR-41 · High · Whitelist rejection rejects whitelisted drops (default drop UI)
- **Where:** `game-client/grimoire/game/DropStack.as:92-104`. For each whitelist entry, the inner `while (i < children)` loop rejects every drop that doesn't contain *that* entry, and `i` is never reset. So only the first entry is honored.
- **Failure:** With whitelist `["Gold Voucher 100k", "Tainted Gem"]`, a *Tainted Gem* drop is rejected during the pass for the first entry. Separately, dispatching a reject click removes the child while iterating by index, which skips the next drop. The custom drop UI path (`:74-91`) does exact matching while this path does substring matching.
- **Fix:** Iterate children once (in reverse) and reject when the name matches *no* whitelist entry. Use the same matching rule in both paths.

#### CR-42 · Medium · `getMonster` can target dead monsters
- **Where:** `game-client/grimoire/game/World.as:137`. `a || b && alive` parses as `a || (b && alive)`, so a *name* match ignores `intState > 0`. `getMonsterByName` (`:162`) gets this right.
- **Failure:** `AttackMonster` (used by `Kill`) selects a corpse that matches by name, and combat stalls until the corpse despawns.
- **Fix:** `((monName.indexOf(...) > -1) || name == "*") && mon.dataLeaf.intState > 0`.

#### CR-43 · Medium · "Map loading stuck" can never be detected
- **Where:** `game-client/grimoire/game/Client.as:48` compares `btnBack.visible` (a Boolean) with `Root.trueString` (the string `'"True"'`), which is always false.
- **Failure:** The recovery branches in `AutoRelogin.cs` (`IsClientLoading("MapLoadingStuck")`) and `CmdJoin2` never fire, so a stuck map load after relog is never reloaded.
- **Fix:** `btnBack.visible == true`.

#### CR-44 · Medium · Hand-quoted JSON strings aren't escaped
- **Where:** `game-client/grimoire/Root.as:373-382`, `game/Network.as:86-112`, and every `'"' + value + '"'` return in `game/Player.as` and `game/World.as` (e.g. `getSkillName`, `cell`, `map`).
- **Failure:** A value containing `"` or `\` (passwords, some chat/area strings) produces invalid JSON, so the C# side deserializes `null`. For example, relog fails for passwords containing `"`.
- **Fix:** Return `JSON.stringify(value)`.

#### CR-45 · Low · Loader has no error handling
- **Where:** `game-client/grimoire/Root.as:87-107`. There are no `IOErrorEvent`/`SecurityErrorEvent` listeners and `JSON.parse` is unguarded. If the AQ version endpoint is down, the client stays blank with no message. `onProgress` (`:112`) divides by `bytesTotal`, which can be 0.

#### CR-46 · Low · Per-packet bridge overhead
- **Where:** `game-client/grimoire/Root.as:140-141` (two listeners on `onDebugMessage`; every packet is filtered and sent to C# even when no logger is open) and `:349` (`pext` call does `JSON.stringify(packet)` for every extension response; C# has no `pext` case at `Proxy.cs:252-281`).
- **Fix:** Only forward when a C# consumer has subscribed, and drop `pext`.

#### CR-47 · Low · Dead and broken callbacks
- **Where:** `Root.as:392-396` (`setTitle` treats the `String` `Game.params.sTitle` as a text field and would throw; it's never called). Nine registered callbacks have no C# caller: `GetCellPlayers`, `GetMapItems`, `GetShops`, `IsBusy`, `IsLoggedIn`, `PlayerData`, `RealAddress`, `RealPort`, `setTitle`.

#### CR-48 · Low · Random room number uses bitwise OR
- **Where:** `game-client/grimoire/game/Client.as:216, 253`. `Math.random() * 98999 | 1001` should be `+ 1001`. It yields a biased subset of values and can exceed 99999.

#### CR-49 · Low · Latent null dereferences
- **Where:** `World.as:161` (`getMonsterByName` doesn't check `pMC`, unlike `getMonster`), `World.as:42` (unknown lock key), `World.as:336` (area name without `-`), `Player.as:383` (`uoTree[...]` can be null).

#### CR-50 · Low · `toggleAdvancedOpt` flips persisted preferences
- **Where:** `game-client/grimoire/game/Setting.as:120`. It XOR-toggles the user's saved `litePreference` flags on bot start and stop. A crash between the two calls leaves the user's game options inverted permanently.

### 1.8 Startup and misc

#### CR-51 · Medium · Startup blocks the UI thread on the network
- **Where:** `app/Grimoire/Program.cs:41-49`. `HttpClient.GetStringAsync(...).Result` runs before `Application.Run`. The `HttpClient` isn't disposed, and the default timeout is 100 s.
- **Failure:** With GitHub slow or blocked, the client shows nothing for up to 100 seconds. The version check also scrapes the GitHub HTML for `(14072022)`, which breaks whenever the page layout changes.
- **Fix:** Run the check asynchronously after the form shows, and query the GitHub Releases API with a short timeout.

#### CR-52 · Low · Sync-over-async in buy-back
- **Where:** `app/Grimoire.Tools.Buyback/AutoBuyBack.cs:78` (`PostAsync(...).Result` inside an `async` method) and `:100` (`Dispose` → `.Wait()`). These are safe today only because the caller runs on the thread pool.

#### CR-53 · Low · Chat log rewrites usernames inside words
- **Where:** `app/Grimoire.Networking.Handlers/HandlerChat.cs` — `Replace(Player.Username.ToLower(), "You")` replaces substrings anywhere in the message. It also assumes `Arguments[4..6]` exist.

#### CR-54 · Low · Misc correctness nits
- `app/Grimoire.Botting/Extensions.cs:104`: `Replace(path[0] + "\\", "")` strips every occurrence, not just the prefix.
- `app/Grimoire.Botting/OptionsManager.cs:256-257`: `SetLagKiller()` is called twice, since the setter already calls it.
- `app/Grimoire.UI/BotManager.cs:758/773`: `chkUntarget` is assigned twice.
- `app/Grimoire/Program.cs:54, 58` and `OptionsManager.cs:239`: `!x ? false : true` patterns.

---

## Part 2 — Technical Debt

Each item is scored with the standard framework: **Impact** (how much it slows work), **Risk** (cost of leaving it), and **Effort** (cost to fix), each on 1–5.
**Priority = (Impact + Risk) × (6 − Effort).** Higher means do it sooner.

### 2.1 Prioritized register

| ID | Item | Category | Impact | Risk | Effort | **Priority** |
|---|---|---|:-:|:-:|:-:|:-:|
| TD-01 | Build isn't reproducible from a clean clone | Infrastructure | 5 | 4 | 2 | **36** |
| TD-02 | No error-handling strategy | Code | 4 | 5 | 3 | **27** |
| TD-03 | Dead and commented-out code | Code | 3 | 2 | 1 | **25** |
| TD-04 | Unused and opaque vendored dependencies | Dependency | 2 | 3 | 1 | **25** |
| TD-05 | Bot engine coupled to WinForms controls | Architecture | 4 | 4 | 3 | **24** |
| TD-06 | Global mutable state and duplicate singletons | Architecture | 4 | 4 | 3 | **24** |
| TD-07 | Copy-paste duplication | Code | 3 | 3 | 2 | **24** |
| TD-08 | No tests, no CI, no analyzers | Test | 4 | 4 | 3 | **24** |
| TD-09 | `.gbot` format bound to CLR type names | Architecture | 3 | 5 | 3 | **24** |
| TD-10 | Stale docs and site config | Documentation | 2 | 2 | 1 | **20** |
| TD-11 | Ad-hoc concurrency model | Architecture | 4 | 5 | 4 | **18** |
| TD-12 | Stringly-typed C#↔AS3 bridge | Architecture | 4 | 4 | 4 | **16** |
| TD-13 | 91 near-identical statement classes | Code | 3 | 2 | 3 | **15** |
| TD-14 | Decompiler artifacts throughout | Code | 3 | 2 | 3 | **15** |
| TD-15 | Repository hygiene | Infrastructure | 2 | 1 | 1 | **15** |
| TD-16 | God classes with inline designer code | Architecture | 4 | 3 | 4 | **14** |
| TD-17 | Runtime depends on end-of-life Flash Player ActiveX | Dependency | 5 | 5 | 5 | **10** |
| TD-18 | Duplicated embedded resources | Infrastructure | 1 | 1 | 2 | **8** |

### 2.2 Item details

#### TD-01 · Build isn't reproducible (Priority 36)
- `app/Grimoire.csproj:180-182` references `VisualStudioTabControl.dll` via `..\..\Controls\VisualStudioTabControl\…\bin\Release`, which is outside the repository. `BotManager.cs:70` and `LogForm.cs:43` depend on it, so **a clean clone can't compile.**
- `Grimoire.csproj:156-175` hard-codes absolute GAC `HintPath`s (including a `GAC_32` path for `System.Web`).
- `Grimoire.csproj:3, 126-127, 650-653` require **PostSharp 6.10.7**, a commercial IL weaver that needs a license or registration. It's used only for `ObjectBindingAttribute` (`Grimoire.Utils/ObjectBinding.cs`).
- `LangVersion` is `8.0` globally (`:9`) but `7.3` per configuration (`:50, 58, 81`).
- **Why it matters:** No one, including future you, can verify any fix in this report without first rebuilding the toolchain by hand.
- **Fix:** Vendor or replace the tab control (DarkUI already provides tabs). Switch to plain framework references. Replace `ObjectBinding` with explicit properties that call `Flash.Call`, which also makes the hidden round-trips visible (TD-12). Pin one `LangVersion`. **Effort: ~1–2 days.**
- **Status (2026-10-14): fixed.** `VisualStudioTabControl.dll` is vendored in `app/Dependency/`. The framework references have no `HintPath`. PostSharp is removed: the 12 `[ObjectBinding]` members are properties that call `Grimoire.Utils/GameObject.cs`. `LangVersion` is 8.0 in all configurations, and the configurations are `Debug`/`Release` × `x86`/`x64`. A clean restore and all four configurations build with MSBuild 18 (VS 2026).

#### TD-02 · No error-handling strategy (Priority 27)
- There are about 70 blanket or empty `catch` blocks and 66 `async void` methods. Exceptions are converted to `default` inside `Flash.Call`/`ObjectBinding`, and nothing is logged even though `LogForm` exists. There are no global exception handlers.
- **Why it matters:** This is the root cause of CR-06, CR-24, CR-25 and the "bot just stops" reports. It also hides every other bug in this list from users and maintainers.
- **Fix:** Add global handlers that log to `LogForm` and a file. Add one `SafeFireAndForget(Task, string context)` helper. Make `catch` blocks log, and use `catch (SpecificException)` where a fallback is intended. **Effort: ~2–3 days.**

#### TD-03 · Dead and commented-out code (Priority 25)
- 4,174 commented-out lines (11% of C#). `Grimoire.Networking.Handlers/HandlerJSON.cs` is 728 of 802 lines commented out.
- Tracked but not compiled: `Grimoire.UI/BrowserForm*.cs`, `Form1*.cs`, `ScriptsForm*.cs`, `Grimoire.Properties/Resources.cs`, `GlobalSuppressions.cs`.
- `Grimoire.UI/BotManagerbackup (2).backup` is a stale 4,652-line copy.
- Unreachable code after `return;` (`BotManager.cs:8402-8419`). Unused helpers: `DocConvert.toRtf`, `AutoRelogin.On*`, `Flash.CallHandler`/`FromFlashXml`, `Player.HasTargetByID`. Nine unused AS3 callbacks (CR-47).
- **Why it matters:** Searches return false hits, reviews take longer, and git history already preserves all of it.
- **Fix:** Delete in one mechanical commit. **Effort: < 1 day.**

#### TD-04 · Unused and opaque vendored dependencies (Priority 25)
- `packages.config`: `BrotliSharpLib`, `Portable.BouncyCastle` and `Microsoft.NETCore.Platforms` have **0** references in code. `Microsoft.Office.Interop.Word` is reachable only from dead code (TD-03), and it would require Word to be installed.
- `app/EasyHook32.dll`, `EasyHook64.dll`, `EasyHook32Svc.exe`, `EasyHook64Svc.exe`, `EasyLoad32.dll`, `EasyLoad64.dll` are committed but have **0** references.
- `app/Dependency/*.dll` (DarkUI, FlatTabControl, Unity3.Eyedropper, Flash interop) are vendored without version or license notes; `FlatTabControl` is referenced by neither the csproj nor the code.
- **Why it matters:** Committed native hooking binaries that nothing uses raise antivirus false positives and user-trust questions. Unused packages add attack surface and restore time.
- **Fix:** Remove the unused ones. Record source, version and license for the rest in `app/Dependency/README.md`. **Effort: < 1 day.**
- **Status (2026-10-14): partly fixed.** The unused packages (including the `System.Buffers`/`Memory`/`Numerics.Vectors`/`CompilerServices.Unsafe`/`RuntimeInformation`/`Threading.Tasks.Extensions` set), the EasyHook/EasyLoad binaries and `FlatTabControl.dll` are removed. `Microsoft.Office.Interop.Word` stays. `app/Dependency/README.md` is still to do.

#### TD-05 · Bot engine coupled to WinForms controls (Priority 24)
- `Bot.cs`, `OptionsManager.cs`, `AutoRelogin.cs`, `HandlerDropItem.cs`, `HandlerLoadShop.cs` and others read `BotManager.Instance.chk*.Checked`, `lstItems.Items` and `lstCommands.SelectedIndex` directly, often from worker threads.
- **Why it matters:** The engine can't be tested or reused, cross-thread UI access is undefined behavior in WinForms, and options drift from the saved `Configuration`.
- **Fix:** Pass the `Configuration` the bot was started with, which already carries most of these flags, and raise events for the UI instead of reading controls. **Effort: ~3–5 days, incremental.**

#### TD-06 · Global mutable state and duplicate singletons (Priority 24)
- There are two engines: `Bot.Instance` (`Bot.cs:32`) and `BotManager._activeBotEngine` (`BotManager.cs:37`). There are two configurations: `Configuration.Instance` (`Configuration.cs:8-9`) and the running `instance.Configuration`.
- There are static `Configuration.Tempvariable`, `Bot.Configurations/OldIndex/DropsInInventory`, and static `Player`/`World`/`OptionsManager`/`BotData` state.
- **Why it matters:** This directly produced CR-09 and CR-10, and it makes behavior depend on initialization order.
- **Fix:** Delete `Bot.Instance` and `Configuration.Instance`, and route through `BotManager.ActiveBotEngine`. Move per-run state onto the engine instance. **Effort: ~2 days.**

#### TD-07 · Copy-paste duplication (Priority 24)
- `Config` and `BotClientConfig` are line-for-line twins.
- `BotManager.GenerateConfiguration` and `SaveConfiguration` (`:567-675`) are ~50 duplicate lines. The bot-save logic exists twice (`:1481`, `:1916`), as do `AddTreeNodes` (`:1645`, `:1665`), `Hotkeys.ExecuteTravel`/`Travel.ExecuteTravel`, and `MakeRelativePath`/`MakeRelativePathFrom`.
- The logout→login→connect sequence is repeated 4 times (`Bot.cs:421-461` ×2, `Root.cs:1518-1560` ×2).
- In AS3, the chat-intercept logic is duplicated (`Client.as:199-231` vs `233-267`), and the monster DTO is built three times (`World.as:56-104, 458-476`).
- **Why it matters:** Bugs get fixed in one copy only; CR-21 is exactly that.
- **Fix:** Extract shared helpers. **Effort: ~2 days.**

#### TD-08 · No tests, no CI, no analyzers (Priority 24)
- There's no test project, no `.github/workflows`, no `.editorconfig`, and analyzer warnings are suppressed ad hoc (`GlobalSuppressions.cs`).
- **Why it matters:** None of the fixes above can be regression-checked.
- **Fix:** Once TD-01 is fixed, add a CI build (GitHub Actions, `windows-latest`, MSBuild) and a small xUnit project with characterization tests for the pure logic:
  - config parsing (CR-32)
  - `Bot.IsVar`/`GetVar`/`Value`
  - `Flash.ToFlashXml` escaping (CR-23)
  - `DocConvert.IsBase64Encoded` (CR-33)
  - statement predicates
  - a `.gbot` round-trip that also asserts the CR-01 binder rejects foreign `$type`s

  Enable `Microsoft.VisualStudio.Threading.Analyzers` to flag `async void` and unawaited tasks. **Effort: ~2–3 days.**

#### TD-09 · `.gbot` format bound to CLR type names (Priority 24)
- `TypeNameHandling.All` writes full CLR type names (`Grimoire.Botting.Commands.Misc.Statements.CmdInInventory, Grimoire`) into every bot file. There's no schema version.
- **Why it matters:** Any class rename or namespace cleanup (TD-13, TD-14) breaks every user's saved bots. It's also the surface behind CR-01.
- **Fix:** Add a `SerializationBinder` that maps stable short names (`"InInventory"`) to types, with a legacy-name table so old files keep loading. Add `"FormatVersion"` to `Configuration`. **Effort: ~2 days. Do it together with CR-01.**

#### TD-10 · Stale docs and site config (Priority 20)
- `docs/_config.yml` sets site-wide `title: "Changelog"` and `permalink: /changelog/`, so every new page collides at the same URL unless it overrides them. This report had to set its own `permalink`.
- `README.md` describes a RAR package and a Discord tag, has no build instructions, and has no architecture overview.
- The version is hard-coded in several places: `Program.cs:16` (`versionDate`), `"Grimlite Rev v1.3"` strings in `Program.cs` and `Root.as:129`.
- **Fix:** Move per-page settings into `index.md` front matter. Add a "Building from source" section and a one-page architecture sketch (UI → `BotManager` → `Bot` → commands → `Flash` → AS3 `Externalizer` → game). Centralize the version. **Effort: < 1 day.**

#### TD-11 · Ad-hoc concurrency model (Priority 18)
- Bot steps run on a thread-pool task and call the STA Flash ActiveX control from that thread (`Flash.Call` → `CallFunction`), relying on COM marshaling.
- Waiting is done by polling (`WaitUntil`, 250–1000 ms sleeps). Cancellation is per-object CTS fields rather than one token, and many loops are fire-and-forget.
- **Why it matters:** This produces nondeterministic races (CR-08, CR-13, CR-16, CR-40), leaked waiters (CR-27), and makes ActiveX calls from the wrong apartment.
- **Fix:** Use one `CancellationToken` per bot run, threaded through `IBotCommand.Execute(IBotEngine, CancellationToken)`. Route all `Flash.Call` invocations through the UI thread (`Control.Invoke`) or a dedicated STA dispatcher. Where AS3 already emits events (packets, map load), replace polling with event-based waits. **Effort: ~1–2 weeks, incremental.**

#### TD-12 · Stringly-typed C#↔AS3 bridge (Priority 16)
- 168 AS3 callbacks are invoked by string literal from about 140 C# call sites. Booleans travel as `'"True"'`, strings are hand-quoted (CR-44), there are two divergent `Flash.Call` families (CR-23), and `ObjectBinding` turns field reads into hidden synchronous round-trips. There's no contract check: two C# wrappers already call callbacks that don't exist (CR-28), and nine callbacks are orphaned (CR-47).
- **Fix:** Generate or maintain a single `GameApi` facade with one method per callback. Have AS3 always return `JSON.stringify(...)`. Add a CI script that diffs `addCallback("…")` names against C# usages. **Effort: ~1 week.**

#### TD-13 · 91 near-identical statement classes (Priority 15)
- `Grimoire.Botting.Commands.Misc.Statements/` holds 94 files. Most are `Is`/`IsNot` or `GreaterThan`/`LessThan` pairs whose only difference is the comparison: `if (!cond) instance.Index++`.
- **Fix:** Add a generic `StatementCommand` with a predicate key and a comparison operator, and use the TD-09 binder to map legacy type names onto it. **Effort: ~3 days. Depends on TD-09.**

#### TD-14 · Decompiler artifacts (Priority 15)
- The namespace-per-folder layout (`Grimoire.Botting.Commands.Misc.Statements/`), decompiler identifiers (`_loc2_`, `_arg_1`, `_local_4` in AS3; `items14`/`items15`, `num`, `text2` in C#), `async` methods with no `await` (CS1998), explicit backing-field boilerplate, and `!x ? false : true`.
- **Fix:** Rename opportunistically when touching a file. Namespace moves must wait for TD-09. **Effort: ongoing.**

#### TD-15 · Repository hygiene (Priority 15)
- `app/.gitignore` has stale entries for another project (`Grimoire3.8PlusPro`, `.vs/Grimoire3.8PlusPlus/…`, `dcatgirl`), and there's no root `.gitignore`.
- `app/LICENSE` duplicates the root `LICENSE`.
- `app/app.config` and `app/Properties/App.config` are both in the csproj (`:601-603`) and differ.
- The SWF is committed with no documented way to rebuild it from the FLA and AS3.
- **Fix:** Clean up `.gitignore`, keep one LICENSE, merge the app configs, and document the SWF build. **Effort: < 1 day.**

#### TD-16 · God classes with inline designer code (Priority 14)
- `BotManager.cs` is 8,776 lines, of which roughly 5,800 are `InitializeComponent` inlined (there's no `.Designer.cs`), with about 100 auto-named controls (`darkGroupBox29`, `darkLabel12`).
- `Root.cs` is 1,789 lines, `Travel.cs` 1,304, and `Grabber.cs` 739.
- **Why it matters:** Merge conflicts, slow navigation, and the Visual Studio designer struggles with the file.
- **Fix:** Move `InitializeComponent` and fields into `BotManager.Designer.cs` (mechanical), then extract each tab into a `UserControl`. **Effort: designer split < 1 day; tab extraction ~1 week.**

#### TD-17 · Runtime depends on end-of-life Flash Player ActiveX (Priority 10)
- The whole client hosts `AxShockwaveFlash` (`Root.cs:142-176`) and checks for it via the registry (`Program.cs:63-72`). Adobe ended Flash Player support on 31 Dec 2020, and stock Windows no longer ships the ActiveX control.
- **Why it matters:** This is the long-term existential risk for the client, and it's outside this codebase's control.
- **Decision needed:** Treat the project as archived and frozen, or port to a different game runtime. A port would be a rewrite of the hosting layer and the AS3 bridge. **Effort: very high. Not recommended unless development resumes.**

#### TD-18 · Duplicated embedded resources (Priority 8)
- Nine `.resx` files are ≥ 500 KB each (5.6 MB of `.resx` in total). Most are byte-identical in size (554,929 bytes), which suggests the same embedded icon or image is serialized per form.
- **Fix:** Reference one image from `Properties/Resources.resx`. **Effort: < 1 day.**

### 2.3 Phased remediation plan

The README states that development stopped on 14 July 2022, so the plan is staged so you can stop after any phase.

**Phase 0 — Security hotfix (do even if the project stays archived)**
- CR-01 together with the binder half of TD-09.
- CR-02 (allow only http/https in `Process.Start`).
- Publish a patch release and add a README note telling users not to load untrusted `.gbot` files on older versions.

**Phase 1 — Make it buildable, plus one-line fixes (~1 week)**
- TD-01, TD-03, TD-04, TD-15, TD-10.
- High bugs that are one- or two-line changes: CR-07, CR-09, CR-10, CR-11, CR-14, CR-17, CR-21, CR-22, CR-41, CR-42, CR-43, and CR-32 (parser fix).

**Phase 2 — Stabilize (~2 weeks)**
- TD-08: CI plus characterization tests, written *before* touching the engine.
- TD-02: error handling and logging, which resolves CR-06, CR-24 and CR-25.
- TD-06: remove duplicate singletons.
- Remaining High/Medium bugs: CR-08, CR-12, CR-13, CR-15, CR-16, CR-18, CR-19, CR-20, CR-23, CR-30, CR-31, CR-33, CR-34.

**Phase 3 — Structural work, alongside any feature work**
- TD-05, TD-07, TD-11, TD-12, TD-16, TD-13 (after TD-09), TD-14, TD-18.

**Strategic:** decide on TD-17 before investing in Phase 3.

---

## Appendix

### A. Files read in full
`app/Grimoire/Program.cs`, `app/Grimoire.Botting/{Bot, BotUtilities, OptionsManager, Configuration (partial), Config, BotClientConfig, Extensions}.cs`, `app/Grimoire.Networking/Proxy.cs`, `app/Grimoire.Tools/{Flash, AutoRelogin, ChatCommand (partial), KeyboardHook, Spammer, DocToRtf (partial)}.cs`, `app/Grimoire.Tools.Plugins/GrimoirePlugin.cs`, `app/Grimoire.Tools.Buyback/AutoBuyBack.cs`, `app/Grimoire.Game/{Player, World, DropStack}.cs`, `app/Grimoire.Networking.Handlers/{HandlerJSON (live code), HandlerXT, HandlerDropItem, HandlerLogin, HandlerLoadShop, HandlerChat, HandlerXtJoin, HandlerQuestComplete, HandlerAFK}.cs`, `app/Grimoire.Botting.Commands.Combat/CmdKill.cs`, `app/Grimoire.Botting.Commands.Map/CmdJoin.cs`, `app/Grimoire.Botting.Commands.Misc/CmdLoadBot.cs`, `app/Grimoire.Botting.Commands.Item/CmdEquipSet.cs`, `app/Grimoire.Utils/ObjectBinding.cs` (partial), `app/Grimoire.UI/BotManager.cs` (all logic outside `InitializeComponent`), `app/Grimoire.UI/Root.cs` (logic sections), `app/Grimoire.UI/PluginManager.cs` (logic), `app/Grimoire.csproj`, `app/packages.config`; `game-client/grimoire/{Root, Externalizer}.as`, `game/{Client, Network, World, Player, Setting, DropStack, Skill}.as`.

### B. Files skimmed or pattern-swept
All other `app/**/*.cs` (the statement and command classes, `Grimoire.Game.Data` models, the remaining UI forms including `Travel`, `Hotkeys`, `LogForm`, `Loaders`, `PacketSpammer`, `Grabber`) and the remaining AS3 files (`Aura`, `Bank`, `House`, `Inventory`, `Quest`, `Shop`, `TempInventory`, `tools/SFSEvent`).

### C. Out of scope
- Binaries: `*.dll`, `*.exe`, `grimlite-rev.swf`, `grimlite-rev.fla`, images. They were inventoried for TD-04 and TD-18 only.
- The git-ignored `(C#) Source Code - Inverse/` and `(AS3) Source Code - Inverse/` folders. About 15 C# and 3 AS3 files there differ from the tracked code beyond line endings; reconcile or delete them separately.
- Gameplay or terms-of-service considerations of a bot client. This review covers code quality, safety for the client's own users, and maintainability only.
