DROP TABLE IF EXISTS public."Game Content";
CREATE TABLE public."Game Content" (
	id bigint NOT NULL GENERATED ALWAYS AS IDENTITY,
	contentId uuid NOT NULL,
	game varchar NOT NULL,
	description varchar NOT NULL,
	contentJson jsonb NOT NULL
);

ALTER TABLE public."Game Content" ADD CONSTRAINT game_content_unique_identifier_contentid UNIQUE (contentId);

-- Column comments
COMMENT ON COLUMN public."Game Content".id IS 'Identifier';
COMMENT ON COLUMN public."Game Content".contentId IS 'Content Id';
COMMENT ON COLUMN public."Game Content".game IS 'Game Version';
COMMENT ON COLUMN public."Game Content".description IS 'Description Of The Default Content';
COMMENT ON COLUMN public."Game Content".contentJson IS 'Content Object';