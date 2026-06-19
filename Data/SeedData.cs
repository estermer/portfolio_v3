using EricstermerCom.Models;
using Microsoft.EntityFrameworkCore;

namespace EricstermerCom.Data;

public static class SeedData
{
    public static async Task RunAsync(IServiceProvider services)
    {
        var db = services.GetRequiredService<AppDbContext>();

        if (!await db.Projects.AnyAsync())
            await SeedProjectsAsync(db);

        if (!await db.Skills.AnyAsync())
            await SeedSkillsAsync(db);

        if (!await db.Recommendations.AnyAsync())
            await SeedRecommendationsAsync(db);

        if (!await db.SiteUsers.AnyAsync())
            await SeedAdminUserAsync(services, db);

        await db.SaveChangesAsync();
    }

    private static async Task SeedAdminUserAsync(IServiceProvider services, AppDbContext db)
    {
        var config = services.GetRequiredService<IConfiguration>();
        var email = config["Admin:Email"] ?? "admin@ericstermer.com";
        var password = config["Admin:Password"] ?? "Admin123!";
        var displayName = config["Admin:DisplayName"] ?? "Eric Stermer";

        var user = new SiteUser
        {
            Email = email,
            DisplayName = displayName,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
        };
        db.SiteUsers.Add(user);

        var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("SeedData");
        logger.LogWarning("Seeded admin user '{Email}'. CHANGE THE DEFAULT PASSWORD before going to production.", email);
    }

    private static async Task SeedProjectsAsync(AppDbContext db)
    {
        var projects = new List<Project>
        {
            new() { Title = "Digital Onboarding", Description = "Using React and Node for digitally streamlining business onboarding journeys", TechTags = "contributing", ImageUrl = "images/projects/digitalonboarding.png", Url = "https://www.digitalonboarding.com/", SortOrder = 10 },
            new() { Title = "Datarista Inc.", Description = "First professional React and Java Application (Search Frame and Dashboard)", TechTags = "contributing", ImageUrl = "images/projects/datarista.png", Url = "http://www.datarista.com/", SortOrder = 20 },
            new() { Title = "Blockstack", Description = "Open Source React Contributor to decentralize the internet using the blockchain", TechTags = "contributing", ImageUrl = "images/projects/blockstack.jpg", Url = "https://github.com/estermer/blockstack.org", SortOrder = 30 },
            new() { Title = "If Me", Description = "Open Source Rails Contributor to help provide a place for support for those who suffer from mental illness", TechTags = "contributing", ImageUrl = "images/projects/ifme.jpg", Url = "https://github.com/estermer/ifme", SortOrder = 40 },
            new() { Title = "FreeCodeCamp", Description = "A free bootcamp to learn fullstack web development", TechTags = "learning", ImageUrl = "images/projects/freecodecamp.png", Url = "https://www.freecodecamp.com/estermer", SortOrder = 50 },
            new() { Title = "Codepen", Description = "One of my coding playgrounds", TechTags = "learning", ImageUrl = "images/projects/codepen.png", Url = "http://codepen.io/estermer/", SortOrder = 60 },
            new() { Title = "Khan Academy", Description = "Introduction to web development", TechTags = "learning", ImageUrl = "images/projects/khanacademy.png", Url = "https://www.khanacademy.org/profile/kaid_1090611702257418820503017/projects", SortOrder = 70 },
            new() { Title = "Codewars", Description = "Improving algorithm skills with friends", TechTags = "learning", ImageUrl = "images/projects/codewars.jpg", Url = "https://www.codewars.com/users/estermer", SortOrder = 80 },
            new() { Title = "Hacker Rank", Description = "Improving algorithm skills", TechTags = "learning", ImageUrl = "images/projects/hackerrank.jpg", Url = "https://www.hackerrank.com/eric_stermer", SortOrder = 90 },
            new() { Title = "Pollster", Description = "An app to create and participate in polls.", TechTags = "rails", ImageUrl = "images/projects/pollster.jpg", Url = "https://github.com/estermer/pollster-ui", SortOrder = 100 },
            new() { Title = "NITPIC", Description = "A photo sharing application.", TechTags = "rails", ImageUrl = "images/projects/nitpic.png", Url = "https://github.com/estermer/nitpic-ui", SortOrder = 110 },
            new() { Title = "RIPTA Reviews", Description = "HackRI 2016 submission for REV RIPTA competition.", TechTags = "rails", ImageUrl = "images/projects/ripta.png", Url = "https://github.com/estermer/ripta-reviews", SortOrder = 120 },
            new() { Title = "Shop Mate", Description = "Track your shopping list for your trip to the grocery store.", TechTags = "node", ImageUrl = "images/projects/shopmate.png", Url = "https://github.com/estermer/grocery-list-app", SortOrder = 130 },
            new() { Title = "Mile Logger", Description = "Track and log your miles you run.", TechTags = "node", ImageUrl = "images/projects/milelogger.jpg", Url = "https://github.com/estermer/mile-logger", SortOrder = 140 },
            new() { Title = "Pokedex", Description = "Keep track of pokemon using NodeJS", TechTags = "node", ImageUrl = "images/projects/pokedex.jpeg", Url = "https://github.com/estermer/eric-pokedex-server", SortOrder = 150 },
            new() { Title = "Add-me", Description = "Big Red Hack-a-thon best business idea at Cornell University, Fall 2016.", TechTags = "node", ImageUrl = "images/projects/addme.png", Url = "https://github.com/JonathanGrant/AddMeForReal", SortOrder = 160 },
            new() { Title = "Black Jack", Description = "A simple game of Black Jack.", TechTags = "jquery", ImageUrl = "images/projects/blackjack.jpg", Url = "https://github.com/estermer/BlackJack-GA", SortOrder = 170 },
            new() { Title = "Liberty Notepad Generator", Description = "A generator for standardized notes when taking student calls", TechTags = "jquery", ImageUrl = "images/projects/libertygen.jpg", Url = "https://codepen.io/estermer/pen/wWXLaz", SortOrder = 180 },
            new() { Title = "Pomodoro Clock", Description = "A stopwatch for short sprints of work and rest.", TechTags = "jquery", ImageUrl = "images/projects/pomodoro.jpeg", Url = "https://codepen.io/estermer/pen/ZOwWYj", SortOrder = 190 },
            new() { Title = "Calculator", Description = "A simple calculator", TechTags = "jquery", ImageUrl = "images/projects/calc.jpg", Url = "https://codepen.io/estermer/pen/LkmqkJ", SortOrder = 200 },
            new() { Title = "Weather App", Description = "Using jQuery AJAX for my first API request app", TechTags = "jquery", ImageUrl = "images/projects/weatherapp.jpg", Url = "https://codepen.io/estermer/pen/wWvJrV", SortOrder = 210 },
        };

        await db.Projects.AddRangeAsync(projects);
    }

    private static async Task SeedSkillsAsync(AppDbContext db)
    {
        var skills = new List<Skill>
        {
            new() { Category = "Languages & Frameworks", Name = "HTML", SortOrder = 10 },
            new() { Category = "Languages & Frameworks", Name = "CSS", SortOrder = 20 },
            new() { Category = "Languages & Frameworks", Name = "JavaScript (ES5 & ES6)", SortOrder = 30 },
            new() { Category = "Languages & Frameworks", Name = "TypeScript", SortOrder = 40 },
            new() { Category = "Languages & Frameworks", Name = "jQuery", SortOrder = 50 },
            new() { Category = "Languages & Frameworks", Name = "React", SortOrder = 60 },
            new() { Category = "Languages & Frameworks", Name = "Ruby on Rails", SortOrder = 70 },
            new() { Category = "Languages & Frameworks", Name = "Node.js", SortOrder = 80 },
            new() { Category = "Languages & Frameworks", Name = "AngularJS", SortOrder = 90 },
            new() { Category = "Languages & Frameworks", Name = "Express.js", SortOrder = 100 },
            new() { Category = "Languages & Frameworks", Name = "MongoDB", SortOrder = 110 },
            new() { Category = "Languages & Frameworks", Name = "SQL", SortOrder = 120 },
            new() { Category = "Languages & Frameworks", Name = "Java", SortOrder = 130 },
            new() { Category = "Languages & Frameworks", Name = "Bootstrap", SortOrder = 140 },
            new() { Category = "Languages & Frameworks", Name = "Salesforce Lightning", SortOrder = 150 },
            new() { Category = "Languages & Frameworks", Name = "Apex (SFDC)", SortOrder = 160 },
            new() { Category = "Languages & Frameworks", Name = "C# / .NET", SortOrder = 170 },
            new() { Category = "Tools & Deployment", Name = "Git", SortOrder = 10 },
            new() { Category = "Tools & Deployment", Name = "GitHub / GitLab", SortOrder = 20 },
            new() { Category = "Tools & Deployment", Name = "Command Line", SortOrder = 30 },
            new() { Category = "Tools & Deployment", Name = "Vagrant", SortOrder = 40 },
            new() { Category = "Tools & Deployment", Name = "NPM", SortOrder = 50 },
            new() { Category = "Tools & Deployment", Name = "Webpack", SortOrder = 60 },
            new() { Category = "Tools & Deployment", Name = "Maven", SortOrder = 70 },
            new() { Category = "Tools & Deployment", Name = "Gradle", SortOrder = 80 },
            new() { Category = "Tools & Deployment", Name = "Docker", SortOrder = 90 },
            new() { Category = "Tools & Deployment", Name = "Azure", SortOrder = 100 },
            new() { Category = "Methodologies", Name = "Object-Oriented Programming", SortOrder = 10 },
            new() { Category = "Methodologies", Name = "MVC Pattern", SortOrder = 20 },
            new() { Category = "Methodologies", Name = "Test-Driven Development", SortOrder = 30 },
            new() { Category = "Methodologies", Name = "Responsive Design", SortOrder = 40 },
            new() { Category = "Methodologies", Name = "Authentication & Authorization", SortOrder = 50 },
            new() { Category = "Methodologies", Name = "User Stories", SortOrder = 60 },
            new() { Category = "Methodologies", Name = "ERDs", SortOrder = 70 },
            new() { Category = "Methodologies", Name = "Wireframing", SortOrder = 80 },
            new() { Category = "Methodologies", Name = "Agile / Scrum", SortOrder = 90 },
        };

        await db.Skills.AddRangeAsync(skills);
    }

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
            new()
            {
                AuthorName = "Patrick McCarty",
                AuthorTitle = "Student, GA Web Development Immersive",
                Content = "Eric is an amazing instructor! Dedicated, prepared and patient. He taught me a ton in a short time. He has a great teaching style and I will be forever grateful for his contributions to my education.",
                LinkedInUrl = "https://www.linkedin.com/in/patrick-mccarty/",
                SortOrder = 30
            },
        };

        await db.Recommendations.AddRangeAsync(recs);
    }
}
