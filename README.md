# ericstermer.com

Source for [ericstermer.com](https://www.ericstermer.com) — a single-page personal site that positions Eric Stermer as a Director of IT Operations, with a developer-to-leadership career arc.

Built with **.NET 10** and **Blazor** (server-side rendering, no client interactivity), backed by **Entity Framework Core** (SQLite in dev, PostgreSQL in prod), and self-hosted from a homelab server.

## What's here

- **Single-page design** — one route (`/`), anchored sections for `#about`, `#experience`, `#work`, `#contact`. No client-side routing, no SPA framework.
- **Dark / light theme** — `data-theme` toggle on `<html>`, persisted to `localStorage`. CSS custom properties in `wwwroot/app.css` drive every color.
- **Admin panel** at `/admin/login` — edit projects, skills, and recommendations live without redeploying. Cookie auth, BCrypt password hashing, antiforgery on forms.
- **SEO ready** — `SeoMeta` component renders Open Graph + Twitter Card tags from the live request; `/sitemap.xml` endpoint serves the canonical URL.
- **Dockerized** — multi-stage `Dockerfile` produces a non-root runtime image; `docker-compose.yml` wires the app to Postgres with persistent volumes for DB and data-protection keys.

## Stack

| Layer            | Tech                                                         |
| ---------------- | ------------------------------------------------------------ |
| Framework        | .NET 10, ASP.NET Core, Blazor (server-rendered)              |
| Data             | EF Core 10 — SQLite (dev), Npgsql / PostgreSQL 16 (prod)     |
| Auth             | Cookie auth (`EricstermerCookie`), BCrypt password hashing   |
| UI               | Razor components, scoped CSS, Space Grotesk font (self-hosted) |
| Container        | Docker (multi-stage), docker-compose                         |

## Project layout

```
.
├── Components/          # Top-level Blazor components (App, Routes, SeoMeta, NotFound)
├── Features/            # Vertical-slice feature folders
│   ├── Home/            # Public single-page site
│   ├── Experience/      # Timeline data
│   ├── Projects/        # Featured projects (used by /admin)
│   ├── Recommendations/ # Testimonials
│   ├── Skills/          # Skills (used by /admin)
│   └── Admin/           # /admin/* — login, projects, skills, recommendations
├── Shared/Layout/       # NavMenu, SiteFooter
├── Data/                # AppDbContext, migrations, SeedData
├── Models/              # EF Core entity models
├── wwwroot/             # Static assets (CSS, images, fonts, favicon)
├── Dockerfile
├── docker-compose.yml
└── Program.cs           # DI wiring, middleware, endpoint mapping
```

## Run it locally

### Quickest: SQLite (no DB setup)

```bash
dotnet restore
dotnet run
```

Open <http://localhost:5000>. The app auto-creates `ericstermer.db`, applies migrations, and seeds projects, skills, and the admin user.

The first-run admin is created from environment variables (or `appsettings.Development.json`):

```
Admin__Email=admin@ericstermer.com
Admin__Password=changeme
```

### With Postgres + Docker

```bash
cp .env.example .env
# edit .env — set POSTGRES_PASSWORD and ADMIN_PASSWORD at minimum

docker compose up --build
```

The app waits on the `db` healthcheck before starting. Data lives in named volumes (`postgres_data`, `app_data`, `app_keys`).

### Reset the dev DB

```bash
rm ericstermer.db ericstermer.db-shm ericstermer.db-wal
dotnet run
```

Seeds re-run on next start when the tables are empty.

## Deployment

The container runs on port 8080 behind a reverse proxy (Caddy, in production). Required env vars:

| Var                  | Purpose                                                       |
| -------------------- | ------------------------------------------------------------- |
| `ConnectionStrings__Default` | EF connection string (Postgres)                        |
| `Admin__Email`       | First-run admin email                                         |
| `Admin__Password`    | First-run admin password                                      |
| `Site__BaseUrl`      | Used by `SeoMeta` to build absolute `og:url` and canonical tags |
| `APP_PORT`           | Host port mapped to container's 8080 (default `8080`)         |

Data-protection keys persist to `/app/.aspnet` (mounted as the `app_keys` volume). Don't lose that volume — losing it signs every visitor out.

## Admin panel

`/admin/login` → cookie session, 7-day sliding expiration. Sections:

- `/admin` — counts of projects, skills, recommendations
- `/admin/projects` — CRUD for the three featured projects on the public site
- `/admin/skills` — CRUD for skills by category
- `/admin/recommendations` — CRUD for testimonials

No delete-without-confirm, no bulk import/export. Keep it boring.

## Design notes

- **Tokens, not classes.** Every color, radius, and shadow is a CSS custom property on `:root` / `[data-theme="light"]`. Components never hardcode colors.
- **Scoped CSS per component.** Each `.razor` has a matching `.razor.css`. Global rules live in `wwwroot/app.css`.
- **No JS framework.** The only JS is a ~15-line theme toggle in `App.razor`. Everything else is server-rendered.
- **Self-hosted Space Grotesk** in `wwwroot/fonts/` — no Google Fonts request, no third-party CDN at runtime.

## License

No license declared. All rights reserved by the author.

If you want to fork it for your own portfolio, that's fine — but please don't clone the content (bio, experience, photos) verbatim. The code is meant to be reused; the personal content is not.