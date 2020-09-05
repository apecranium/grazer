# grazer - personal blog application

this repository contains an application that can be used as a template or referenced for a private blog using asp.net core. it demonstrates the following features:

- custom bootstrap theme using node-sass
- code-first database generation using entity framework with postgresql
- asp.net core identity without identityui or code generation packages
- wysiwyg post editor using summernote

this project was made and tested on x64 linux using visual studio code.

**install**

run `dotnet tool restore` to restore the local tools (namely powershell) unless you have powershell installed locally.

run `grazer.ps1 install` once from the root directory to install required npm packages and copy css/js to the web root.

run `grazer.ps1 initdb` to initialize the database.

there are two important configuration settings you can add either in appsettings, environment variables, or user secrets:

1. `Mailbox` which sets the mailbox that emails are sent from.
2. `AdminPassword` which is the password set for the root admin account created during database generation. defaults to `admin`
