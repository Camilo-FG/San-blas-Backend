-- Tabla para CMS del landing (ejecutar en Supabase si la migración EF no está aplicada)
CREATE TABLE IF NOT EXISTS "LandingContents" (
    "Id" SERIAL PRIMARY KEY,
    "SectionKey" VARCHAR(64) NOT NULL UNIQUE,
    "JsonData" TEXT NOT NULL,
    "UpdatedAt" TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW()
);
