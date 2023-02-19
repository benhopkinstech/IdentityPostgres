CREATE SCHEMA identity
    AUTHORIZATION postgres;
--------------------------------------------------
CREATE TABLE identity.config_mail_provider
(
    id smallint NOT NULL,
    name character varying(20) NOT NULL,
    CONSTRAINT pk_config_mail_provider PRIMARY KEY (id)
);

ALTER TABLE IF EXISTS identity.config_mail_provider
    OWNER to postgres;

INSERT INTO identity.config_mail_provider (id, name) VALUES (1, 'SendGrid');
--------------------------------------------------
CREATE TABLE identity.account_provider
(
    id smallint NOT NULL,
    name character varying(20) NOT NULL,
    CONSTRAINT pk_account_provider PRIMARY KEY (id)
);

ALTER TABLE IF EXISTS identity.account_provider
    OWNER to postgres;

INSERT INTO identity.account_provider (id, name) VALUES (0, 'Local Identity');
--------------------------------------------------
CREATE TABLE identity.config_mail_type
(
    id smallint NOT NULL,
    name character varying(20) NOT NULL,
    CONSTRAINT pk_config_mail_type PRIMARY KEY (id)
);

ALTER TABLE IF EXISTS identity.config_mail_type
    OWNER to postgres;

INSERT INTO identity.config_mail_type (id, name) VALUES (0, 'Test');
INSERT INTO identity.config_mail_type (id, name) VALUES (1, 'Email Verification');
--------------------------------------------------
CREATE TABLE identity.config_mail
(
    id uuid NOT NULL,
    provider_id smallint NOT NULL,
    api_key character varying(256) NOT NULL,
    email character varying(256) NOT NULL,
    name character varying(70) NOT NULL,
    created_on timestamp with time zone NOT NULL DEFAULT (now() at time zone 'utc'),
    updated_on timestamp with time zone,
    CONSTRAINT pk_config_mail PRIMARY KEY (id),
    CONSTRAINT u_config_mail UNIQUE (provider_id),
    CONSTRAINT fk_config_mail_config_mail_provider FOREIGN KEY (provider_id)
        REFERENCES identity.config_mail_provider (id)
);

ALTER TABLE IF EXISTS identity.config_mail
    OWNER to postgres;
--------------------------------------------------
CREATE TABLE identity.config
(
    id smallint NOT NULL DEFAULT 0,
    mail_id uuid,
    account_verification_required bool NOT NULL,
    updated_on timestamp with time zone DEFAULT (now() at time zone 'utc'),
    CONSTRAINT pk_config PRIMARY KEY (id),
    CONSTRAINT chk_config CHECK (id = 0),
    CONSTRAINT fk_config_config_mail FOREIGN KEY (mail_id)
        REFERENCES identity.config_mail (id)
);

ALTER TABLE IF EXISTS identity.config
    OWNER to postgres;

INSERT INTO identity.config (account_verification_required) VALUES (false);
--------------------------------------------------
CREATE TABLE identity.config_mail_template
(
    id uuid NOT NULL,
    mail_id uuid NOT NULL,
    type_id smallint NOT NULL,
    provider_template_identifier character varying(100) NOT NULL,
    created_on timestamp with time zone NOT NULL DEFAULT (now() at time zone 'utc'),
    updated_on timestamp with time zone,
    CONSTRAINT pk_config_mail_template PRIMARY KEY (id),
    CONSTRAINT u_config_mail_template UNIQUE (mail_id, type_id),
    CONSTRAINT fk_config_mail_template_config_mail FOREIGN KEY (mail_id)
        REFERENCES identity.config_mail (id),
    CONSTRAINT fk_config_mail_template_config_mail_type FOREIGN KEY (type_id)
        REFERENCES identity.config_mail_type (id)
);

ALTER TABLE IF EXISTS identity.config_mail_template
    OWNER to postgres;
--------------------------------------------------
CREATE TABLE identity.account
(
    id uuid NOT NULL,
    provider_id smallint NOT NULL DEFAULT 0,
    email character varying(256) NOT NULL,
    verified boolean NOT NULL DEFAULT false,
    verified_on timestamp with time zone,
    created_on timestamp with time zone NOT NULL DEFAULT (now() at time zone 'utc'),
    updated_on timestamp with time zone,
    CONSTRAINT pk_account PRIMARY KEY (id),
    CONSTRAINT u_account UNIQUE (provider_id, email),
    CONSTRAINT fk_account_account_provider FOREIGN KEY (provider_id)
        REFERENCES identity.account_provider (id)
);

ALTER TABLE IF EXISTS identity.account
    OWNER to postgres;
--------------------------------------------------
CREATE TABLE identity.account_password
(
    account_id uuid NOT NULL,
    hash character varying(60) NOT NULL,
    updated_on timestamp with time zone,
    CONSTRAINT pk_account_password PRIMARY KEY (account_id),
    CONSTRAINT fk_account_password_account FOREIGN KEY (account_id)
        REFERENCES identity.account (id)
);

ALTER TABLE IF EXISTS identity.account_password
    OWNER to postgres;
--------------------------------------------------
CREATE TABLE identity.account_login
(
    id bigserial NOT NULL,
    account_id uuid,
    email character varying(256) NOT NULL,
    successful boolean NOT NULL DEFAULT false,
    ip_address inet,
    created_on timestamp with time zone NOT NULL DEFAULT (now() at time zone 'utc'),
    CONSTRAINT pk_account_login PRIMARY KEY (id),
    CONSTRAINT fk_account_login_account FOREIGN KEY (account_id)
        REFERENCES identity.account (id)
);

ALTER TABLE IF EXISTS identity.account_login
    OWNER to postgres;