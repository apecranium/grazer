#!/bin/bash

# install packages for frontend
npm install
mkdir -p wwwroot/css
mkdir -p wwwroot/js

# bootstrap
cp -u node_modules/bootstrap/dist/js/bootstrap.bundle.min.js wwwroot/js

# jquery, jquery validate, jquery validate unobtrusive
cp -u node_modules/jquery/dist/jquery.min.js wwwroot/js
cp -u node_modules/jquery-validation/dist/jquery.validate.min.js wwwroot/js
cp -u node_modules/jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.min.js wwwroot/js

# summernote
cp -u node_modules/summernote/dist/summernote-bs4.min.js wwwroot/js
cp -u node_modules/summernote/dist/summernote-bs4.min.css wwwroot/css
cp -ru node_modules/summernote/dist/font wwwroot/css
