# Portfolio Redesign: Single-Page "Story Arc"

**Date:** 2026-07-28
**Status:** Approved
**Goal:** Redesign ericstermer.com from a multi-page bootcamp portfolio into a single-page personal brand site that positions Eric as a Director of IT Operations with a developer-to-leadership career arc.

---

## 1. Context & Motivation

The current site has 5 separate pages showcasing 21 bootcamp-era projects, a flat list of individual tech skills, and a hero that reads "Software Engineer." Eric's actual career has progressed from Software Developer to Technical Lead to Director of IT Operations. The site doesn't reflect this.

**Primary goal:** Personal brand and credibility -- not job hunting. The site should feel authoritative, refined, and minimal. Visitors should leave thinking "this person is senior and knows what they're doing."

---

## 2. Design Decisions

| Decision | Choice | Rationale |
|---|---|---|
| Page structure | Single-page scroll | Tells the career story linearly; higher engagement than multi-page for medium content |
| Design language | Refined & minimal, dark default | Confident, spacious, typography-led. Think Vercel/Linear dark mode |
| Color system | Indigo accent, dark/light toggle | Already implemented. Keeps the techy feel without being gimmicky |
| Typography | Space Grotesk | Already implemented. Sharp, geometric, modern |
| Hero treatment | Typography-only, no photo | At director level, the title and tagline carry more authority than a headshot |
| Photo placement | About section, right column | Humanizes after the reader is already impressed |
| Projects approach | 3-4 curated impact cards, no images | Impact statements > screenshots at this level. No filtering needed |
| Recommendations | Compact 2-card section, not a full page | CTO and CEO quotes are strong; student quote dropped |
| Career timeline | Hardcoded in Razor | No admin editing needed for now; avoids new data model |
| Visual effects | Subtle only | Hover transitions, gentle shadows. No animations beyond the existing fade-in-up |
| Border radius | 12px (current) | Rounded but not pill-shaped |

---

## 3. Page Sections (top to bottom)

### 3.1 Navigation

- Sticky top bar with frosted glass background (`var(--nav-bg)`)
- "ES" logo mark (indigo) + "Eric Stermer" text
- Anchor links: `About`, `Experience`, `Work`, `Contact` (smooth-scroll to sections)
- Theme toggle button (sun/moon, already implemented)
- On mobile: logo mark only, links shrink, same behavior as current responsive nav

### 3.2 Hero

- Full viewport height (`min-height: 100vh`), content vertically centered
- All text centered horizontally, `max-width: 36rem`
- Layout stack (top to bottom):
  - **Eyebrow:** "Software Engineering & IT Leadership" -- small, uppercase, letter-spaced, `var(--accent)` color
  - **Name:** "Eric Stermer" -- `clamp(2.5rem, 5vw, 4rem)`, weight 700, tight letter-spacing (`-0.02em`)
  - **Title:** "Director of IT Operations" -- `1.25rem`, `var(--accent)`, weight 500
  - **Tagline:** One sentence spanning the IC-to-director arc -- `1.125rem`, `var(--text-muted)`, `line-height: 1.7`
  - **CTAs:** Two buttons: "See my work" (primary/filled) + "Get in touch" (ghost/outline). Both scroll to respective sections.
- Subtle `fade-in-up` animation on load (existing keyframe)
- No decorative elements, no background patterns. Whitespace is the design.

### 3.3 About

- Two-column layout: 60% text / 40% photo on desktop. Stacks on mobile (photo first on mobile for visual hierarchy).
- Section title "About" with indigo left-bar accent (`section-title::before`)
- 2-3 paragraphs covering: origin story (code to GA to first job), career arc (developer to tech lead to director), what drives him now (teams, systems, operational excellence), personal color (Providence, runner, foodie)
- Photo: `border-radius: var(--radius)` (12px, not circle), `var(--shadow)`, natural aspect ratio
- Text: `1rem`, `line-height: 1.7`, `var(--text)`

### 3.4 Career Timeline

- Section title "Experience" with accent left-bar
- Vertical line: `2px` wide, `var(--border)` color, runs down the left side
- Timeline entries (most recent first):
  - **Dot marker:** `12px` circle, `var(--accent)`, positioned on the vertical line
  - **Card:** `.card` style (surface bg, border, shadow), offset to the right of the line
  - **Card content:** Role title (bold, `var(--text)`), Company + date range (`var(--text-muted)`), 2-3 bullet accomplishments
- Entries are hardcoded in Razor. Placeholder content:
  1. Director of IT Operations -- [Company] -- 2023-Present
  2. Technical Lead -- [Company] -- 2020-2023
  3. Software Developer -- [Company] -- 2017-2020
  4. (Optional) Earlier roles or General Assembly
- Mobile: line stays left-aligned, cards go full width

### 3.5 Competencies Grid

- Section title "Skills & Competencies" with accent left-bar
- CSS Grid: `repeat(auto-fit, minmax(280px, 1fr))` -- 3 columns desktop, 2 tablet, 1 mobile
- 4-6 domain cards, each a `.card` with:
  - Thin top-border accent (`3px solid var(--accent)`)
  - Domain title: bold, `var(--text)`
  - Specific techs/skills underneath: `var(--text-muted)`, `0.875rem`
- Placeholder domains:
  1. Infrastructure & Operations (AWS, Azure, Docker, CI/CD, monitoring)
  2. Full-Stack Development (React, Node, .NET/Blazor, Rails, SQL)
  3. Team Leadership & Mentorship (hiring, coaching, code review, agile/scrum)
  4. Cloud & DevOps (Terraform, k8s, GitHub Actions)
  5. Architecture & Systems Design (API design, microservices, scalability)
  6. IT Operations (vendor management, budgets, security, compliance)
- Uses existing `Skill` model data grouped by `Category`. Seed data will need updating to reflect these domains.

### 3.6 Selected Work

- Section title "Selected Work" with accent left-bar
- Full-width stacked cards (not a grid -- each gets room to breathe)
- Each card: `.card` with `4px` left accent border
  - Title: bold, `1.125rem`
  - Tech tags: small pills (`var(--accent-soft)` bg, `var(--accent)` text)
  - Description: 2-3 sentences focused on impact, `var(--text)`, `1rem`
  - Optional: external link if project is public
- No images in cards. Impact statements are the visual.
- No category filter pills (irrelevant for 3-4 items)
- Uses existing `Project` model. `ImageUrl` field stays in model but is not rendered. Seed data replaced with real entries.
- Placeholder entries:
  1. Cloud Infrastructure Migration -- AWS, Terraform, Docker, CI/CD
  2. Internal Developer Platform -- .NET, React, Azure, DevOps
  3. IT Operations Overhaul -- monitoring, vendor consolidation, security
  4. (Optional 4th)

### 3.7 Testimonials

- Section title "What Others Say" with accent left-bar
- Two-column grid on desktop, stacks on mobile
- 2 cards (CTO and CEO quotes), each a `.card` with `4px` left accent border:
  - Quote text: italic, `var(--text)`, `1rem`, `line-height: 1.7`
  - Author: right-aligned, name bold + linked to LinkedIn, title in `var(--text-muted)`
- No decorative quotation marks. The italic text and accent border signal "quote."
- Uses existing `Recommendation` model. Drop the student recommendation from seed data.

### 3.8 Contact

- Section title "Get in Touch" with accent left-bar
- Short intro line: "Have a question or want to connect? Drop me a line." in `var(--text-muted)`
- Existing `ContactForm.razor` component, centered, `max-width: 32rem`
  - Keeps floating-label pattern -- it fits the minimal aesthetic
  - Keeps `@rendermode InteractiveServer`
  - No backend changes
- Below form: horizontal divider with "or find me on" text, then social icon row (X, GitHub, LinkedIn)

### 3.9 Footer

- Minimal: thin top border, single centered line
- "© 2026 Eric Stermer. Built with .NET & Blazor."
- No social icons (they're in the contact section now)
- Small font, `var(--text-muted)`

---

## 4. Technical Implementation

### Files Modified

| File | Change |
|---|---|
| `Features/Home/Home.razor` | Complete rewrite -- becomes the single-page with all sections |
| `Features/Home/Home.razor.css` | Complete restyle for all sections |
| `Features/Home/Services/GetFeaturedProjects.cs` | Rename or keep; still fetches projects for the Work section |
| `Shared/Layout/NavMenu.razor` | Change nav links to `#anchor` smooth-scroll links |
| `Shared/Layout/NavMenu.razor.css` | Minor update for scroll-anchored active states (stretch goal) |
| `Shared/Layout/SiteFooter.razor` | Simplify -- remove social icons, copyright only |
| `Shared/Layout/SiteFooter.razor.css` | Minimal update |
| `Data/SeedData.cs` | Update project and skill seed data to reflect real content |

### Files Unchanged

| File | Reason |
|---|---|
| `Features/Admin/*` | Admin panel is separate, behind its own layout |
| `Features/Contact/Components/ContactForm.razor` | Reused as-is inside the home page |
| `Features/Contact/Contact.razor.css` | Form styles still apply |
| `Models/*` | All data models stay the same |
| `Data/AppDbContext.cs` | No schema changes |
| `Shared/Layout/MainLayout.razor` | Shell structure unchanged |
| `Shared/Layout/MainLayout.razor.css` | Design tokens already updated |
| `wwwroot/app.css` | Global resets already updated |
| `Components/App.razor` | Font + theme script already done |

### Files to Remove or Deprecate

| File | Action |
|---|---|
| `Features/Experience/Experience.razor` | Remove route; content moves to Home |
| `Features/Experience/Experience.razor.css` | No longer needed |
| `Features/Projects/Projects.razor` | Remove route; content moves to Home |
| `Features/Projects/Projects.razor.css` | No longer needed |
| `Features/Projects/Components/ProjectFilter.razor` | No longer needed (no filtering) |
| `Features/Projects/Components/ProjectFilter.razor.css` | Remove |
| `Features/Projects/Components/ProjectCard.razor` | Could reuse or inline in Home |
| `Features/Recommendations/Recommendations.razor` | Remove route; content moves to Home |
| `Features/Recommendations/Recommendations.razor.css` | No longer needed |
| `Features/Contact/Contact.razor` | Remove route; form component reused in Home |
| `Features/Contact/Contact.razor.css` | May be partially reused |

### Smooth Scrolling

- Add `scroll-behavior: smooth` to `html` in `app.css`
- Nav links become `<a href="#about">`, `<a href="#experience">`, etc.
- Each section gets a corresponding `id` attribute
- `scroll-padding-top: var(--nav-height)` on `html` to account for sticky nav

### Services Reuse

The Home page will inject and call:
- `GetSkills` -- for the competencies grid
- `GetProjects` (or `GetFeaturedProjects`) -- for the selected work section
- `GetRecommendations` -- for the testimonials section
- `ContactForm` component handles its own service injection

---

## 5. Content Placeholders

The following content needs to be provided by Eric. The implementation will use realistic placeholder text that clearly marks what needs replacing:

- [ ] Hero tagline (one sentence, IC-to-director arc)
- [ ] About bio (2-3 paragraphs)
- [ ] Career timeline entries (3-4 roles with company, dates, 2-3 bullets each)
- [ ] Competency domain groupings (4-6 categories with specific skills)
- [ ] Selected work entries (3-4 projects with title, tags, impact description)
- [ ] Updated recommendations (confirm which 2 to keep, or add new ones)

---

## 6. Out of Scope

- Blog / articles section
- Scroll-spy active highlighting in nav (nice-to-have, can add later)
- Page transition animations beyond the existing `fade-in-up`
- Any backend/API changes
- Admin panel redesign
- SEO meta tag updates (can do as a follow-up)
