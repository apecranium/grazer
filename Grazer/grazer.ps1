#!/usr/bin/env pwsh
param(
  $mode = "build"
)

$js = @(
  "node_modules/bootstrap/dist/js/bootstrap.bundle.min.js",
  "node_modules/jquery/dist/jquery.min.js",
  "node_modules/jquery-validation/dist/jquery.validate.min.js",
  "node_modules/jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.min.js",
  "node_modules/summernote/dist/summernote-bs4.min.js"
)

$css = @(
  "node_modules/summernote/dist/summernote-bs4.min.css"
)

$font = @(
  "node_modules/summernote/dist/font"
)

function CopyFiles([string] $dest, [array] $files) {
  $files | % {
    cp -u $_ $dest
  }
}

function CopyDirs([string] $dest, [array] $dirs) {
  $dirs | % {
    cp -ru $_ $dest
  }
}

# Commands
function Build {
  echo "build started"
  npm run sass
  dotnet build
}

function Install {
  echo "installing"
  npm i
  dotnet restore
  mkdir -p wwwroot/css
  mkdir -p wwwroot/js
  CopyFiles "wwwroot/js" $js
  CopyFiles "wwwroot/css" $css
  CopyDirs "wwwroot/css" $font
}

function InitDb {
  echo "initializing database"
  createdb grazer
  dotnet ef database update
}

function RebuildDataModel {
  echo "rebuilding database"
  rm -r Migrations
  dropdb grazer
  createdb grazer
  dotnet ef migrations add "Initial_Migration"
  dotnet ef database update
}

Push-Location $PSScriptRoot
if ($mode -eq "build") {
  Build
}
elseif ($mode -eq "install") {
  Install
}
elseif ($mode -eq "initdb") {
  InitDb
}
elseif ($mode -eq "rebuilddb") {
  RebuildDataModel
}
else {
  write-error "unknown command $mode"
}
Pop-Location
