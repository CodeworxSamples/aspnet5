/// <binding ProjectOpened='sass:watch, typescript:watch' />
	// Include all the necessary plugins
var gulp = require("gulp");
var concat = require("gulp-concat");
var uglify = require("gulp-uglify");
var del = require("del");
var sass = require("gulp-sass");
var ts = require("gulp-typescript");
var sourcemaps = require("gulp-sourcemaps");
var changed = require("gulp-changed");
var newer = require('gulp-newer');
var minifycss = require("gulp-minify-css");

// Helper arrays holding file and folder names for later use in this gruntfile
// External script dependencies
var dependencyScripts = ["bower_components/jquery/dist/jquery.js", "bower_components/angularjs/angular.js",
	"bower_components/angular-ui-grid/ui-grid.js", "bower_components/angular-ui-router/release/angular-ui-router.js",
	"bower_components/bootstrap/dist/js/bootstrap.js", "bower_components/angular-bootstrap/ui-bootstrap-tpls.js"];
// External style dependencies
var dependencyStylesheets = ["bower_components/angular-ui-grid/ui-grid.css", "bower_components/bootstrap/dist/css/bootstrap.css",
	"bower_components/bootstrap/dist/css/bootstrap-theme.css"];
// External font dependencies
var bootstrapFonts = ["bower_components/bootstrap/dist/fonts/*.*"];
// Refererence to Bootstrap CSS (note that unminified bootstrap version is copied to styles folder
// so that Visual Studio IntelliSense works for CSS classes)
var bootstrapCss = ["bower_components/bootstrap/dist/css/bootstrap.css"];
// Dependencies for UI Grid component
var uiGridResources = ["bower_components/angular-ui-grid/ui-grid.eot", "bower_components/angular-ui-grid/ui-grid.svg",
	"bower_components/angular-ui-grid/ui-grid.ttf", "bower_components/angular-ui-grid/ui-grid.woff"];
// Custom stylesheet for this application
var customStylesheets = ["wwwroot/components/styles/styles.scss"];
// TypeScript sources
var typescriptFiles = ["wwwroot/**/*.ts"];

// Delete build targets to clean up
gulp.task("clean", function () {
	del.sync(["wwwroot/scripts/**/*.*"]);
	del.sync(["wwwroot/styles/*.*"]);
	del.sync(["wwwroot/fonts/*.*"]);
	del.sync(["wwwroot/styles/*.*"]);
});

// Combine and minify all external scripts
gulp.task("dependencyScriptsAndStyles", [], function () {
	// External scripts
	gulp.src(dependencyScripts)
		.pipe(newer("wwwroot/scripts/dependencies.min.js"))
		.pipe(uglify())
		.pipe(concat("dependencies.min.js"))
		.pipe(gulp.dest("wwwroot/scripts/"));

	// External styles
	gulp.src(dependencyStylesheets)
		.pipe(newer("wwwroot/styles/dependencies.min.css"))
		.pipe(minifycss())
		.pipe(concat("dependencies.min.css"))
		.pipe(gulp.dest("wwwroot/styles/"));

	// Copy bootstrap CSS (not minified) for Visual Studio IntelliSense
	gulp.src(bootstrapCss)
		.pipe(changed("wwwroot/styles/"))
		.pipe(gulp.dest("wwwroot/styles/"));

	// External fonts
	gulp.src(bootstrapFonts)
		.pipe(changed("wwwroot/fonts/"))
		.pipe(gulp.dest("wwwroot/fonts/"));

	// Copy additional resources (e.g. fonts) for UI Grid component
	gulp.src(uiGridResources)
		.pipe(changed("wwwroot/styles/"))
		.pipe(gulp.dest("wwwroot/styles/"));
});

// Process SASS sources and generate CSS
gulp.task("sass", [], function () {
	var sassSettings = {
		sourcemap: true,
		sourcemapPath: "wwwroot/components/styles"
	};

	// Generate minified CSS version
	gulp.src(customStylesheets)
		.pipe(newer("wwwroot/styles/styles.min.css"))
		.pipe(sass(sassSettings))
		.pipe(minifycss())
		.pipe(concat("styles.min.css"))
		.pipe(gulp.dest("wwwroot/styles/"));

	// Generate unminifed CSS version (for Visual Studio IntelliSense)
	gulp.src(customStylesheets)
		.pipe(newer("wwwroot/styles/styles.css"))
		.pipe(sass(sassSettings))
		.pipe(concat("styles.css"))
		.pipe(gulp.dest("wwwroot/styles/"));
});

// Process TypeScript files
gulp.task("typescript", [], function () {
	return gulp.src(typescriptFiles)
		.pipe(newer("wwwroot/scripts/application.js"))
		.pipe(sourcemaps.init())
		.pipe(ts({
			noImplicitAny: true,
			out: "application.js"
		}))
		.pipe(sourcemaps.write("./maps"))
		.pipe(gulp.dest("wwwroot/scripts"));
});

// Watch tasks for SASS and TypeScript sources
gulp.task("sass:watch", function () {
	gulp.watch("styles/*.scss", ["sass"]);
});
gulp.task("typescript:watch", function () {
	gulp.watch(["wwwroot/*.ts", "wwwroot/**/*.ts"], ["typescript"]);
});

// Set a default tasks
gulp.task("default", ["clean", "sass", "typescript", "dependencyScriptsAndStyles"], function () { });