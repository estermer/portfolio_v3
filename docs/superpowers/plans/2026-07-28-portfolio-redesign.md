# Portfolio Redesign Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Transform the multi-page bootcamp portfolio into a single-page "Story Arc" site that positions Eric as a Director of IT Operations.

**Architecture:** Single Blazor SSR page at `/` with anchor-scrolled sections (Hero, About, Timeline, Competencies, Work, Testimonials, Contact, Footer). Existing services (`GetSkills`, `GetRecommendations`, `GetFeaturedProjects`) are reused. Contact form component is embedded as-is. Career timeline is hardcoded in Razor. Old page routes are removed.

**Tech Stack:** .NET 10, Blazor SSR, vanilla CSS custom properties (dark/light theme already in place), Space Grotesk font.

## Global Constraints

- No new NuGet packages
- No JavaScript frameworks -- vanilla JS only (theme toggle already done)
- All colors must use CSS custom properties from `MainLayout.razor.css` -- no hardcoded hex values
- Keep `@rendermode InteractiveServer` only on `ContactForm.razor` -- everything else is static SSR
- Admin panel (`/admin/*`) is completely untouched
- All data models (`Project`, `Skill`, `Recommendation`, `ContactSubmission`) are unchanged
- The existing dark/light theme system and Space Grotesk font are already in place -- do not modify `Components/App.razor` or `MainLayout.razor.css` token definitions

---

### Task 1: Add Smooth Scrolling and Update Navigation

**Files:**
- Modify: `wwwroot/app.css`
- Modify: `Shared/Layout/NavMenu.razor`
- Modify: `Shared/Layout/NavMenu.razor.css`

**Interfaces:**
- Consumes: Nothing
- Produces: Nav bar with `#about`, `#experience`, `#work`, `#contact` anchor links. Smooth scroll behavior on `html`.

- [ ] **Step 1: Add smooth scrolling and scroll-padding to app.css**

Add to the `html, body` rule in `wwwroot/app.css`:

```css
html {
    scroll-behavior: smooth;
    scroll-padding-top: 72px; /* nav-height (64px) + 8px breathing room */
}
```

This must be a separate `html` rule (not inside the existing `html, body` block) since `scroll-behavior` only applies to `html`.

- [ ] **Step 2: Update NavMenu.razor to use anchor links**

Replace the full contents of `Shared/Layout/NavMenu.razor` with:

```razor
<header class="nav-root">
    <div class="nav-inner">
        <a class="nav-logo" href="/">
            <span class="nav-logo-mark">ES</span>
            <span class="nav-logo-text">Eric Stermer</span>
        </a>
        <nav class="nav-links" aria-label="Primary">
            <a class="nav-link" href="#about">About</a>
            <a class="nav-link" href="#experience">Experience</a>
            <a class="nav-link" href="#work">Work</a>
            <a class="nav-link" href="#contact">Contact</a>
            <button class="theme-toggle" onclick="toggleTheme()" title="Toggle theme" aria-label="Toggle dark/light theme">
                <svg class="theme-icon-sun" width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                    <circle cx="12" cy="12" r="5"/><line x1="12" y1="1" x2="12" y2="3"/><line x1="12" y1="21" x2="12" y2="23"/><line x1="4.22" y1="4.22" x2="5.64" y2="5.64"/><line x1="18.36" y1="18.36" x2="19.78" y2="19.78"/><line x1="1" y1="12" x2="3" y2="12"/><line x1="21" y1="12" x2="23" y2="12"/><line x1="4.22" y1="19.78" x2="5.64" y2="18.36"/><line x1="18.36" y1="5.64" x2="19.78" y2="4.22"/>
                </svg>
                <svg class="theme-icon-moon" width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                    <path d="M21 12.79A9 9 0 1 1 11.21 3 7 7 0 0 0 21 12.79z"/>
                </svg>
            </button>
        </nav>
    </div>
</header>
```

Key changes: `NavLink` components replaced with plain `<a>` tags (NavLink's active-class matching doesn't work with `#hash` anchors). Links point to `#about`, `#experience`, `#work`, `#contact`. Reduced from 5 links to 4.

- [ ] **Step 3: Remove NavLink active styles from NavMenu.razor.css**

The `.nav-link.active` styles relied on Blazor's `NavLink` component. Replace them with a hover-only interaction. In `Shared/Layout/NavMenu.razor.css`, replace:

```css
.nav-link.active {
    color: var(--accent);
    background: var(--accent-soft);
}

.nav-link.active:hover {
    background: var(--accent-soft);
}
```

with:

```css
.nav-link:focus-visible {
    color: var(--accent);
    outline: 2px solid var(--accent);
    outline-offset: 2px;
}
```

- [ ] **Step 4: Verify the build compiles**

Run: `dotnet build`
Expected: 0 errors. Warnings about SQLitePCLRaw are acceptable.

- [ ] **Step 5: Commit**

```bash
git add wwwroot/app.css Shared/Layout/NavMenu.razor Shared/Layout/NavMenu.razor.css
git commit -m "feat: switch nav to anchor links with smooth scrolling"
```

---

### Task 2: Simplify the Footer

**Files:**
- Modify: `Shared/Layout/SiteFooter.razor`
- Modify: `Shared/Layout/SiteFooter.razor.css`

**Interfaces:**
- Consumes: Nothing
- Produces: Minimal footer with copyright only (social icons move to contact section in Task 4)

- [ ] **Step 1: Replace SiteFooter.razor**

Replace the full contents of `Shared/Layout/SiteFooter.razor` with:

```razor
<footer class="site-footer">
    <div class="site-footer-inner">
        <p class="copyright">&copy; @DateTime.UtcNow.Year Eric Stermer. Built with .NET &amp; Blazor.</p>
    </div>
</footer>
```

- [ ] **Step 2: Simplify SiteFooter.razor.css**

Replace the full contents of `Shared/Layout/SiteFooter.razor.css` with:

```css
.site-footer {
    border-top: 1px solid var(--border);
    padding: 2rem 1.5rem;
}

.site-footer-inner {
    max-width: var(--max-width);
    margin: 0 auto;
    text-align: center;
}

.copyright {
    margin: 0;
    font-size: 0.8125rem;
    color: var(--text-muted);
}
```

- [ ] **Step 3: Verify build**

Run: `dotnet build`
Expected: 0 errors.

- [ ] **Step 4: Commit**

```bash
git add Shared/Layout/SiteFooter.razor Shared/Layout/SiteFooter.razor.css
git commit -m "feat: simplify footer to minimal copyright line"
```

---

### Task 3: Rewrite Home.razor as the Single-Page Layout

**Files:**
- Modify: `Features/Home/Home.razor`

**Interfaces:**
- Consumes: `GetFeaturedProjects.HandleAsync()` returns `IReadOnlyList<Project>`, `GetSkills.HandleAsync()` returns `IReadOnlyList<SkillGroup>`, `GetRecommendations.HandleAsync()` returns `IReadOnlyList<Recommendation>`
- Produces: Single page with sections: `#hero`, `#about`, `#experience` (timeline + competencies), `#work`, `#testimonials`, `#contact`

- [ ] **Step 1: Replace Home.razor with the full single-page layout**

Replace the entire contents of `Features/Home/Home.razor` with:

```razor
@page "/"
@using EricstermerCom.Features.Home.Services
@using EricstermerCom.Features.Experience.Services
@using EricstermerCom.Features.Recommendations.Services
@inject GetFeaturedProjects GetFeaturedProjects
@inject GetSkills GetSkills
@inject GetRecommendations GetRecommendations

<PageTitle>Eric Stermer · Director of IT Operations</PageTitle>
<SeoMeta Title="Eric Stermer · Director of IT Operations"
         Description="Eric Stermer — Director of IT Operations. From writing code to leading engineering teams and IT operations."
         Path="/" />

@* ── Hero ── *@
<section class="hero" id="hero">
    <div class="hero-content">
        <p class="hero-eyebrow">Software Engineering &amp; IT Leadership</p>
        <h1 class="hero-name">Eric Stermer</h1>
        <p class="hero-title">Director of IT Operations</p>
        <p class="hero-tagline">
            From writing my first line of code to leading engineering teams and
            IT operations — I build systems, grow people, and ship what matters.
        </p>
        <div class="hero-actions">
            <a class="btn btn-primary" href="#work">See my work</a>
            <a class="btn btn-ghost" href="#contact">Get in touch</a>
        </div>
    </div>
</section>

@* ── About ── *@
<section class="about" id="about">
    <h2 class="section-title">About</h2>
    <div class="about-grid">
        <div class="about-text">
            <p>
                My journey into tech started with Khan Academy's Hour of Code —
                a moment that sparked a passion I've been building on ever since.
                I went through General Assembly's Web Development Immersive and
                landed my first professional role as a developer at Datarista,
                where I quickly grew from junior developer to full stack engineer.
            </p>
            <p>
                Over the years I've progressed from writing code to leading
                engineering teams to directing IT operations. I've architected
                systems, mentored developers, managed cloud infrastructure, and
                built the processes that keep teams shipping reliably.
            </p>
            <p>
                Outside of work, I'm based in Providence, RI. I'm a former
                competitive distance runner (with a few National Championships
                to my name) and a dedicated foodie working my way through
                Providence's restaurant scene.
            </p>
        </div>
        <div class="about-photo">
            <img src="images/avatar.jpg" alt="Eric Stermer" loading="lazy" />
        </div>
    </div>
</section>

@* ── Career Timeline ── *@
<section class="experience" id="experience">
    <h2 class="section-title">Experience</h2>
    <div class="timeline">

        <div class="timeline-entry">
            <div class="timeline-dot"></div>
            <div class="card timeline-card">
                <h3 class="timeline-role">Director of IT Operations</h3>
                <p class="timeline-meta">[Company Name] · 2023 – Present</p>
                <ul class="timeline-bullets">
                    <li>Leading IT operations and infrastructure strategy</li>
                    <li>Managing engineering teams and cross-functional initiatives</li>
                    <li>Driving operational excellence and system reliability</li>
                </ul>
            </div>
        </div>

        <div class="timeline-entry">
            <div class="timeline-dot"></div>
            <div class="card timeline-card">
                <h3 class="timeline-role">Technical Lead</h3>
                <p class="timeline-meta">[Company Name] · 2020 – 2023</p>
                <ul class="timeline-bullets">
                    <li>Led a team of engineers on full-stack product development</li>
                    <li>Architected scalable systems and CI/CD pipelines</li>
                    <li>Mentored junior and mid-level developers</li>
                </ul>
            </div>
        </div>

        <div class="timeline-entry">
            <div class="timeline-dot"></div>
            <div class="card timeline-card">
                <h3 class="timeline-role">Software Developer</h3>
                <p class="timeline-meta">Datarista Inc. · 2017 – 2020</p>
                <ul class="timeline-bullets">
                    <li>Full-stack development with React, Redux, and Java</li>
                    <li>Promoted from Jr. Developer to Full Stack Engineer</li>
                    <li>Built front-end components and contributed to backend APIs</li>
                </ul>
            </div>
        </div>

    </div>
</section>

@* ── Competencies Grid ── *@
<section class="competencies">
    <h2 class="section-title">Skills &amp; Competencies</h2>
    <div class="competencies-grid">
        @foreach (var group in _skills)
        {
            <div class="card competency-card">
                <h3 class="competency-title">@group.Category</h3>
                <p class="competency-items">
                    @string.Join(", ", group.Skills.Select(s => s.Name))
                </p>
            </div>
        }
    </div>
</section>

@* ── Selected Work ── *@
<section class="work" id="work">
    <h2 class="section-title">Selected Work</h2>
    <div class="work-list">
        @foreach (var project in _projects)
        {
            <article class="card work-card">
                <h3 class="work-title">@project.Title</h3>
                <ul class="work-tags">
                    @foreach (var tag in project.TagList)
                    {
                        <li class="work-tag">@tag</li>
                    }
                </ul>
                <p class="work-desc">@project.Description</p>
                @if (!string.IsNullOrEmpty(project.Url))
                {
                    <a class="work-link" href="@project.Url" target="_blank" rel="noopener noreferrer">
                        View project →
                    </a>
                }
            </article>
        }
    </div>
</section>

@* ── Testimonials ── *@
<section class="testimonials" id="testimonials">
    <h2 class="section-title">What Others Say</h2>
    <div class="testimonials-grid">
        @foreach (var rec in _recommendations.Take(2))
        {
            <blockquote class="card testimonial-card">
                <p class="testimonial-quote">"@rec.Content"</p>
                <footer class="testimonial-author">
                    @if (!string.IsNullOrEmpty(rec.LinkedInUrl))
                    {
                        <a class="testimonial-name" href="@rec.LinkedInUrl" target="_blank" rel="noopener noreferrer">@rec.AuthorName</a>
                    }
                    else
                    {
                        <span class="testimonial-name">@rec.AuthorName</span>
                    }
                    <span class="testimonial-title">@rec.AuthorTitle</span>
                </footer>
            </blockquote>
        }
    </div>
</section>

@* ── Contact ── *@
<section class="contact" id="contact">
    <h2 class="section-title">Get in Touch</h2>
    <p class="contact-intro">Have a question or want to connect? Drop me a line.</p>
    <div class="contact-form-wrap">
        <EricstermerCom.Features.Contact.Components.ContactForm />
    </div>
    <div class="contact-social">
        <span class="contact-social-label">or find me on</span>
        <div class="social-icons">
            <a href="https://twitter.com/RunninStermz" target="_blank" rel="noopener noreferrer" aria-label="Twitter">
                <svg width="20" height="20" viewBox="0 0 24 24" fill="currentColor" aria-hidden="true">
                    <path d="M18.244 2H21.5l-7.5 8.57L23 22h-6.844l-5.36-7.01L4.6 22H1.34l8.02-9.17L1 2h7.02l4.84 6.4L18.244 2zm-1.2 18h1.86L7.04 4H5.06l11.984 16z"/>
                </svg>
            </a>
            <a href="https://github.com/estermer" target="_blank" rel="noopener noreferrer" aria-label="GitHub">
                <svg width="20" height="20" viewBox="0 0 24 24" fill="currentColor" aria-hidden="true">
                    <path d="M12 .5C5.65.5.5 5.65.5 12c0 5.08 3.29 9.39 7.86 10.91.58.1.79-.25.79-.56v-2c-3.2.69-3.87-1.36-3.87-1.36-.52-1.34-1.28-1.69-1.28-1.69-1.05-.71.08-.7.08-.7 1.16.08 1.77 1.19 1.77 1.19 1.03 1.77 2.7 1.26 3.36.96.1-.75.4-1.26.74-1.55-2.55-.29-5.24-1.28-5.24-5.7 0-1.26.45-2.29 1.18-3.1-.12-.29-.51-1.46.11-3.04 0 0 .97-.31 3.18 1.18a11.05 11.05 0 0 1 5.79 0c2.21-1.49 3.18-1.18 3.18-1.18.63 1.58.23 2.75.11 3.04.74.81 1.18 1.84 1.18 3.1 0 4.43-2.7 5.41-5.27 5.69.41.36.78 1.06.78 2.14v3.17c0 .31.21.67.8.55C20.21 21.39 23.5 17.08 23.5 12 23.5 5.65 18.35.5 12 .5z"/>
                </svg>
            </a>
            <a href="https://www.linkedin.com/in/eric-stermer-37b74122/" target="_blank" rel="noopener noreferrer" aria-label="LinkedIn">
                <svg width="20" height="20" viewBox="0 0 24 24" fill="currentColor" aria-hidden="true">
                    <path d="M19 0H5C2.24 0 0 2.24 0 5v14c0 2.76 2.24 5 5 5h14c2.76 0 5-2.24 5-5V5c0-2.76-2.24-5-5-5zM7.5 19h-3V9h3v10zM6 7.5C4.9 7.5 4 6.6 4 5.5S4.9 3.5 6 3.5 8 4.4 8 5.5 7.1 7.5 6 7.5zM20 19h-3v-5c0-1.2-.4-2-1.5-2-1.1 0-1.5.8-1.5 2v5h-3V9h3v1.4c.5-.8 1.5-1.6 3-1.6 2.2 0 3 1.4 3 4V19z"/>
                </svg>
            </a>
        </div>
    </div>
</section>

@code {
    private IReadOnlyList<Models.Project> _projects = Array.Empty<Models.Project>();
    private IReadOnlyList<SkillGroup> _skills = Array.Empty<SkillGroup>();
    private IReadOnlyList<Models.Recommendation> _recommendations = Array.Empty<Models.Recommendation>();

    protected override async Task OnInitializedAsync()
    {
        var projectsTask = GetFeaturedProjects.HandleAsync();
        var skillsTask = GetSkills.HandleAsync();
        var recsTask = GetRecommendations.HandleAsync();

        await Task.WhenAll(projectsTask, skillsTask, recsTask);

        _projects = await projectsTask;
        _skills = await skillsTask;
        _recommendations = await recsTask;
    }
}
```

- [ ] **Step 2: Verify the build compiles**

Run: `dotnet build`
Expected: 0 errors. The page now references `GetSkills` and `GetRecommendations` which are already registered in `Program.cs`.

- [ ] **Step 3: Commit**

```bash
git add Features/Home/Home.razor
git commit -m "feat: rewrite home page as single-page story arc layout"
```

---

### Task 4: Restyle Home.razor.css for All Sections

**Files:**
- Modify: `Features/Home/Home.razor.css`

**Interfaces:**
- Consumes: CSS custom properties from `MainLayout.razor.css` (both light and dark theme tokens)
- Produces: Complete styles for hero, about, timeline, competencies, work, testimonials, and contact sections

- [ ] **Step 1: Replace Home.razor.css with the full single-page stylesheet**

Replace the entire contents of `Features/Home/Home.razor.css` with:

```css
/* ═══════════════════════════════════════════
   Hero
   ═══════════════════════════════════════════ */

.hero {
    min-height: 100vh;
    display: flex;
    align-items: center;
    justify-content: center;
    text-align: center;
    padding: 2rem 1.5rem;
}

.hero-content {
    max-width: 38rem;
}

.hero-eyebrow {
    margin: 0 0 1rem;
    font-size: 0.8125rem;
    font-weight: 600;
    text-transform: uppercase;
    letter-spacing: 0.1em;
    color: var(--accent);
}

.hero-name {
    margin: 0 0 0.5rem;
    font-size: clamp(2.5rem, 5vw, 4rem);
    font-weight: 700;
    line-height: 1.05;
    letter-spacing: -0.02em;
    color: var(--text);
}

.hero-title {
    margin: 0 0 1.5rem;
    font-size: 1.25rem;
    font-weight: 500;
    color: var(--accent);
}

.hero-tagline {
    margin: 0 0 2rem;
    font-size: 1.125rem;
    color: var(--text-muted);
    line-height: 1.7;
}

.hero-actions {
    display: flex;
    flex-wrap: wrap;
    justify-content: center;
    gap: 0.75rem;
}

/* ═══════════════════════════════════════════
   Buttons (shared)
   ═══════════════════════════════════════════ */

.btn {
    display: inline-flex;
    align-items: center;
    justify-content: center;
    padding: 0.65rem 1.25rem;
    border-radius: 10px;
    font-size: 0.9375rem;
    font-weight: 500;
    text-decoration: none;
    border: 1px solid transparent;
    transition: background-color 0.15s ease, border-color 0.15s ease, transform 0.15s ease;
}

.btn:hover {
    transform: translateY(-1px);
}

.btn-primary {
    background: var(--accent);
    color: var(--text-on-accent);
}

.btn-primary:hover {
    background: var(--accent-hover);
    color: var(--text-on-accent);
}

.btn-ghost {
    background: transparent;
    color: var(--text);
    border-color: var(--border);
}

.btn-ghost:hover {
    background: var(--surface-hover);
    color: var(--text);
}

/* ═══════════════════════════════════════════
   About
   ═══════════════════════════════════════════ */

.about {
    padding: 4rem 0;
}

.about-grid {
    display: grid;
    grid-template-columns: 1fr 0.65fr;
    gap: 3rem;
    align-items: start;
}

.about-text p {
    margin: 0 0 1rem;
    font-size: 1rem;
    line-height: 1.7;
    color: var(--text);
}

.about-text p:last-child {
    margin-bottom: 0;
}

.about-photo img {
    width: 100%;
    border-radius: var(--radius);
    box-shadow: var(--shadow);
}

@media (max-width: 720px) {
    .about-grid {
        grid-template-columns: 1fr;
        gap: 2rem;
    }

    .about-photo {
        order: -1;
        max-width: 240px;
    }
}

/* ═══════════════════════════════════════════
   Career Timeline
   ═══════════════════════════════════════════ */

.experience {
    padding: 4rem 0;
}

.timeline {
    position: relative;
    padding-left: 2rem;
    margin-top: 1.5rem;
}

.timeline::before {
    content: "";
    position: absolute;
    left: 5px;
    top: 0;
    bottom: 0;
    width: 2px;
    background: var(--border);
}

.timeline-entry {
    position: relative;
    margin-bottom: 1.5rem;
}

.timeline-entry:last-child {
    margin-bottom: 0;
}

.timeline-dot {
    position: absolute;
    left: -2rem;
    top: 1.5rem;
    width: 12px;
    height: 12px;
    background: var(--accent);
    border-radius: 50%;
    transform: translateX(-3px);
}

.timeline-card {
    transition: transform 0.2s ease, box-shadow 0.2s ease;
}

.timeline-card:hover {
    transform: translateY(-2px);
    box-shadow: var(--shadow-hover);
}

.timeline-role {
    margin: 0 0 0.25rem;
    font-size: 1.125rem;
    font-weight: 600;
    color: var(--text);
}

.timeline-meta {
    margin: 0 0 0.75rem;
    font-size: 0.875rem;
    color: var(--text-muted);
}

.timeline-bullets {
    margin: 0;
    padding-left: 1.25rem;
    list-style: disc;
}

.timeline-bullets li {
    font-size: 0.9375rem;
    line-height: 1.6;
    color: var(--text);
    margin-bottom: 0.25rem;
}

.timeline-bullets li:last-child {
    margin-bottom: 0;
}

/* ═══════════════════════════════════════════
   Competencies
   ═══════════════════════════════════════════ */

.competencies {
    padding: 4rem 0;
}

.competencies-grid {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
    gap: 1rem;
    margin-top: 1.5rem;
}

.competency-card {
    border-top: 3px solid var(--accent);
}

.competency-title {
    margin: 0 0 0.5rem;
    font-size: 1rem;
    font-weight: 600;
    color: var(--text);
}

.competency-items {
    margin: 0;
    font-size: 0.875rem;
    color: var(--text-muted);
    line-height: 1.6;
}

/* ═══════════════════════════════════════════
   Selected Work
   ═══════════════════════════════════════════ */

.work {
    padding: 4rem 0;
}

.work-list {
    display: flex;
    flex-direction: column;
    gap: 1.25rem;
    margin-top: 1.5rem;
}

.work-card {
    border-left: 4px solid var(--accent);
    transition: transform 0.2s ease, box-shadow 0.2s ease;
}

.work-card:hover {
    transform: translateY(-2px);
    box-shadow: var(--shadow-hover);
}

.work-title {
    margin: 0 0 0.5rem;
    font-size: 1.125rem;
    font-weight: 600;
    color: var(--text);
}

.work-tags {
    list-style: none;
    padding: 0;
    margin: 0 0 0.75rem;
    display: flex;
    flex-wrap: wrap;
    gap: 0.35rem;
}

.work-tag {
    font-size: 0.6875rem;
    font-weight: 500;
    text-transform: uppercase;
    letter-spacing: 0.04em;
    color: var(--accent);
    background: var(--accent-soft);
    padding: 0.2rem 0.55rem;
    border-radius: 999px;
}

.work-desc {
    margin: 0;
    font-size: 1rem;
    color: var(--text);
    line-height: 1.7;
}

.work-link {
    display: inline-block;
    margin-top: 0.75rem;
    font-size: 0.875rem;
    font-weight: 500;
    color: var(--accent);
}

.work-link:hover {
    color: var(--accent-hover);
}

/* ═══════════════════════════════════════════
   Testimonials
   ═══════════════════════════════════════════ */

.testimonials {
    padding: 4rem 0;
}

.testimonials-grid {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(320px, 1fr));
    gap: 1.25rem;
    margin-top: 1.5rem;
}

.testimonial-card {
    border-left: 4px solid var(--accent);
    display: flex;
    flex-direction: column;
    justify-content: space-between;
}

.testimonial-quote {
    margin: 0 0 1rem;
    font-style: italic;
    font-size: 1rem;
    line-height: 1.7;
    color: var(--text);
}

.testimonial-author {
    display: flex;
    flex-direction: column;
    align-items: flex-end;
    gap: 0.1rem;
}

.testimonial-name {
    font-weight: 600;
    font-style: normal;
    color: var(--text);
    text-decoration: none;
}

a.testimonial-name:hover {
    color: var(--accent);
}

.testimonial-title {
    font-size: 0.8125rem;
    font-style: italic;
    color: var(--text-muted);
}

/* ═══════════════════════════════════════════
   Contact
   ═══════════════════════════════════════════ */

.contact {
    padding: 4rem 0;
}

.contact-intro {
    margin: 0 0 2rem;
    color: var(--text-muted);
    font-size: 1rem;
}

.contact-form-wrap {
    max-width: 32rem;
}

.contact-social {
    display: flex;
    align-items: center;
    gap: 1rem;
    margin-top: 2.5rem;
    padding-top: 2rem;
    border-top: 1px solid var(--border);
}

.contact-social-label {
    font-size: 0.8125rem;
    color: var(--text-muted);
    white-space: nowrap;
}

.social-icons {
    display: flex;
    align-items: center;
    gap: 0.5rem;
}

.social-icons a {
    display: inline-flex;
    align-items: center;
    justify-content: center;
    width: 40px;
    height: 40px;
    border-radius: 999px;
    color: var(--text-muted);
    transition: color 0.15s ease, background-color 0.15s ease, transform 0.15s ease;
}

.social-icons a:hover {
    color: var(--accent);
    background: var(--accent-soft);
    transform: translateY(-2px);
}
```

- [ ] **Step 2: Verify build**

Run: `dotnet build`
Expected: 0 errors.

- [ ] **Step 3: Commit**

```bash
git add Features/Home/Home.razor.css
git commit -m "feat: add complete styles for single-page portfolio sections"
```

---

### Task 5: Update Seed Data

**Files:**
- Modify: `Data/SeedData.cs`

**Interfaces:**
- Consumes: `Project`, `Skill`, `Recommendation` models (unchanged)
- Produces: Updated seed data with director-level placeholder projects, domain-level skill categories, and 2 recommendations (CTO + CEO only)

- [ ] **Step 1: Update the SeedProjectsAsync method**

In `Data/SeedData.cs`, replace the `SeedProjectsAsync` method body (the `var projects = new List<Project> { ... };` block inside the method) with:

```csharp
    private static async Task SeedProjectsAsync(AppDbContext db)
    {
        var projects = new List<Project>
        {
            new() { Title = "Cloud Infrastructure Migration", Description = "Led the migration of on-premise infrastructure to AWS, reducing deployment time by 80% and cutting hosting costs by 40%. Designed the CI/CD pipeline architecture and managed the phased rollout across engineering teams.", TechTags = "AWS, Terraform, Docker, CI/CD, GitHub Actions", ImageUrl = "", Url = null, SortOrder = 10 },
            new() { Title = "Internal Developer Platform", Description = "Architected and built internal tooling used by engineers daily for deployment automation, environment provisioning, and monitoring. Reduced onboarding time for new developers from days to hours.", TechTags = ".NET, React, Azure, DevOps, PostgreSQL", ImageUrl = "", Url = null, SortOrder = 20 },
            new() { Title = "IT Operations Overhaul", Description = "Consolidated vendor relationships, implemented centralized monitoring, and established security compliance processes. Improved system uptime to 99.9% and reduced incident response time by 60%.", TechTags = "Infrastructure, Monitoring, Security, Vendor Management", ImageUrl = "", Url = null, SortOrder = 30 },
        };

        await db.Projects.AddRangeAsync(projects);
    }
```

- [ ] **Step 2: Update the SeedSkillsAsync method**

In `Data/SeedData.cs`, replace the `SeedSkillsAsync` method body with:

```csharp
    private static async Task SeedSkillsAsync(AppDbContext db)
    {
        var skills = new List<Skill>
        {
            new() { Category = "Full-Stack Development", Name = "React", SortOrder = 10 },
            new() { Category = "Full-Stack Development", Name = "Node.js", SortOrder = 20 },
            new() { Category = "Full-Stack Development", Name = "C# / .NET", SortOrder = 30 },
            new() { Category = "Full-Stack Development", Name = "Blazor", SortOrder = 40 },
            new() { Category = "Full-Stack Development", Name = "TypeScript", SortOrder = 50 },
            new() { Category = "Full-Stack Development", Name = "SQL / PostgreSQL", SortOrder = 60 },
            new() { Category = "Full-Stack Development", Name = "Ruby on Rails", SortOrder = 70 },
            new() { Category = "Infrastructure & Operations", Name = "AWS", SortOrder = 10 },
            new() { Category = "Infrastructure & Operations", Name = "Azure", SortOrder = 20 },
            new() { Category = "Infrastructure & Operations", Name = "Docker", SortOrder = 30 },
            new() { Category = "Infrastructure & Operations", Name = "CI/CD Pipelines", SortOrder = 40 },
            new() { Category = "Infrastructure & Operations", Name = "Monitoring & Observability", SortOrder = 50 },
            new() { Category = "Infrastructure & Operations", Name = "Linux Administration", SortOrder = 60 },
            new() { Category = "Team Leadership", Name = "Engineering Team Management", SortOrder = 10 },
            new() { Category = "Team Leadership", Name = "Hiring & Onboarding", SortOrder = 20 },
            new() { Category = "Team Leadership", Name = "Mentorship & Code Review", SortOrder = 30 },
            new() { Category = "Team Leadership", Name = "Agile / Scrum", SortOrder = 40 },
            new() { Category = "Team Leadership", Name = "Cross-Functional Collaboration", SortOrder = 50 },
            new() { Category = "Cloud & DevOps", Name = "Terraform", SortOrder = 10 },
            new() { Category = "Cloud & DevOps", Name = "GitHub Actions", SortOrder = 20 },
            new() { Category = "Cloud & DevOps", Name = "Kubernetes", SortOrder = 30 },
            new() { Category = "Cloud & DevOps", Name = "Infrastructure as Code", SortOrder = 40 },
            new() { Category = "Architecture & Design", Name = "System Architecture", SortOrder = 10 },
            new() { Category = "Architecture & Design", Name = "API Design", SortOrder = 20 },
            new() { Category = "Architecture & Design", Name = "Microservices", SortOrder = 30 },
            new() { Category = "Architecture & Design", Name = "Scalability & Performance", SortOrder = 40 },
            new() { Category = "IT Operations", Name = "Vendor Management", SortOrder = 10 },
            new() { Category = "IT Operations", Name = "Budget & Resource Planning", SortOrder = 20 },
            new() { Category = "IT Operations", Name = "Security & Compliance", SortOrder = 30 },
            new() { Category = "IT Operations", Name = "Incident Response", SortOrder = 40 },
        };

        await db.Skills.AddRangeAsync(skills);
    }
```

- [ ] **Step 3: Update the SeedRecommendationsAsync method**

In `Data/SeedData.cs`, replace the `SeedRecommendationsAsync` method body to keep only the CTO and CEO quotes:

```csharp
    private static async Task SeedRecommendationsAsync(AppDbContext db)
    {
        var recs = new List<Recommendation>
        {
            new()
            {
                AuthorName = "Dave Costantino",
                AuthorTitle = "CTO, Datarista Inc.",
                Content = "We hired Eric right out of General Assembly. I knew in our first conversation that he was smart and could get things done! This proved true throughout his time at Datarista. He initially started first on the front end, working with React and Redux, then moved into a full stack role where he worked on parts of our back end, as well. I highly recommend Eric for a software engineering position.",
                LinkedInUrl = "https://www.linkedin.com/in/davecostantino/",
                SortOrder = 10
            },
            new()
            {
                AuthorName = "Pat Sabatino",
                AuthorTitle = "CEO, Datarista Inc.",
                Content = "We hired Eric for his first professional developer job coming out of the General Assembly Developer Immersive. From the start he was clearly a very smart guy and we felt that with time he would learn the business aspects of being a developer. He quickly adapted his problem solving and critical thinking and we were proud to promote him from Jr. Developer to Full Stack Engineer, a title he rightly earned. He is personable, optimistic and hard working and delivers results. I would highly recommend Eric as a developer.",
                LinkedInUrl = "https://www.linkedin.com/in/patsabatino/",
                SortOrder = 20
            },
        };

        await db.Recommendations.AddRangeAsync(recs);
    }
```

- [ ] **Step 4: Verify build**

Run: `dotnet build`
Expected: 0 errors.

- [ ] **Step 5: Commit**

```bash
git add Data/SeedData.cs
git commit -m "feat: update seed data with director-level content"
```

**Note:** The seed data only runs when tables are empty. To see the new data, either delete the `ericstermer.db` file and restart, or update records via the admin panel.

---

### Task 6: Remove Old Page Files and Update Sitemap

**Files:**
- Delete: `Features/Experience/Experience.razor`
- Delete: `Features/Experience/Experience.razor.css`
- Delete: `Features/Projects/Projects.razor`
- Delete: `Features/Projects/Projects.razor.css`
- Delete: `Features/Projects/Components/ProjectFilter.razor`
- Delete: `Features/Projects/Components/ProjectFilter.razor.css`
- Delete: `Features/Recommendations/Recommendations.razor`
- Delete: `Features/Recommendations/Recommendations.razor.css`
- Delete: `Features/Contact/Contact.razor`
- Delete: `Features/Contact/Contact.razor.css`
- Modify: `Program.cs` (sitemap update)

**Interfaces:**
- Consumes: Nothing
- Produces: Clean file tree with no orphaned pages. Sitemap reflects single-page structure.

- [ ] **Step 1: Delete old page files**

```bash
rm Features/Experience/Experience.razor
rm Features/Experience/Experience.razor.css
rm Features/Projects/Projects.razor
rm Features/Projects/Projects.razor.css
rm Features/Projects/Components/ProjectFilter.razor
rm Features/Projects/Components/ProjectFilter.razor.css
rm Features/Recommendations/Recommendations.razor
rm Features/Recommendations/Recommendations.razor.css
rm Features/Contact/Contact.razor
rm Features/Contact/Contact.razor.css
```

- [ ] **Step 2: Update sitemap in Program.cs**

In `Program.cs`, find the sitemap route handler and replace the `var urls` array:

```csharp
    var urls = new[]
    {
        ("/", "1.0", "weekly"),
    };
```

This replaces the old 5-URL sitemap with a single entry since all content is now on one page.

- [ ] **Step 3: Verify build**

Run: `dotnet build`
Expected: 0 errors. The deleted pages had `@page` directives for `/experience`, `/projects`, `/recommendations`, `/contact` — those routes simply stop existing.

- [ ] **Step 4: Commit**

```bash
git add -A
git commit -m "chore: remove old multi-page files, update sitemap"
```

---

### Task 7: Final Verification

**Files:**
- None (verification only)

**Interfaces:**
- Consumes: All previous tasks
- Produces: Confidence that the site builds and runs

- [ ] **Step 1: Clean build**

```bash
dotnet clean && dotnet build
```

Expected: 0 errors.

- [ ] **Step 2: Check for broken references**

Search for any remaining references to the deleted pages:

```bash
grep -r "href=\"/experience\"\|href=\"/projects\"\|href=\"/recommendations\"\|href=\"/contact\"" --include="*.razor" --include="*.cs" .
```

Expected: No matches in active files. (May match in `archive/` folder which is fine.)

- [ ] **Step 3: Run the site**

```bash
dotnet run
```

Navigate to `http://localhost:5000` (or whatever port it binds to). Verify:
- Hero section loads with centered typography
- Smooth scrolling works via nav links
- About section shows photo + text
- Timeline section shows 3 career entries
- Competencies grid shows 6 domain cards
- Selected Work shows 3 project cards
- Testimonials shows 2 quotes
- Contact form loads and is interactive
- Theme toggle switches between dark and light
- Footer shows minimal copyright

- [ ] **Step 4: Commit any cleanup**

If any small fixes were needed:

```bash
git add -A
git commit -m "fix: final cleanup after redesign verification"
```
