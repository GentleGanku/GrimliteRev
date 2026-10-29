-- Apply with: npx wrangler d1 execute grimliterev --remote --file schema.sql
-- Safe to run again: it only creates what's missing.

-- One row per release file and visitor country.
CREATE TABLE IF NOT EXISTS downloads (
	tag TEXT NOT NULL,
	file TEXT NOT NULL,
	country TEXT NOT NULL,
	count INTEGER NOT NULL DEFAULT 0,
	PRIMARY KEY (tag, file, country)
);

-- One row per page and month.
CREATE TABLE IF NOT EXISTS pageviews (
	path TEXT NOT NULL,
	month TEXT NOT NULL,
	count INTEGER NOT NULL DEFAULT 0,
	PRIMARY KEY (path, month)
);

-- One row per Roadmap vote. voter is a salted SHA-256 hash of the visitor's IP.
CREATE TABLE IF NOT EXISTS votes (
	item TEXT NOT NULL,
	voter TEXT NOT NULL,
	created TEXT NOT NULL,
	PRIMARY KEY (item, voter)
);
