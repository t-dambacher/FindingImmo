# FindingImmo

This is my very own solution for finding my future house, by scraping real estate agencies' websites, prefiltering their ads and then submitting them to me through a website.

## Stack

The app uses the following tools :
* Database : 
  * [EntityFramework Core](https://github.com/aspnet/EntityFrameworkCore)
  * [SQLite](https://www.sqlite.org)
* Web custom
  * [AspNetCore](https://github.com/aspnet/Home)
* Scraping :
  * [Selenium](https://www.seleniumhq.org)

## CI and hosting
[Work In Progress]

## Architecture

The project is structured as a standard 3-tiers app.

## Coding guidelines

To check for code sanity and enforce some behaviour and style, the project uses :
* The .NET-integrated analyzer (for "Managed Binary Analysis")
* The [C# Coding Guidelines](https://www.csharpcodingguidelines.com), customized to suppress some "extrem" rules
* The [EditorConfig](https://editorconfig.org) tool, in order to add more checks, in addition to the previous analyzers, to add some formating at the writing time
 
Those tools were mainly used here to being tryed tested, inside a pseudo-real world context.
