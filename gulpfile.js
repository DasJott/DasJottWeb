// Less configuration
var gulp = require('gulp');
var bundle = require('gulp-bundle-assets');
var rimraf = require('gulp-rimraf');

var publicRoot = "./wwwroot";
var dirMaps = publicRoot + "/maps"

var scriptsSrc = 'Content/js';
var scriptsDst = publicRoot;

// clean up destination directory before bundling and copying new stuff
gulp.task('clean', function () {
  return gulp.src(publicRoot+'/**/*.{js,css,map}', { read: false })
    .pipe(rimraf());
});

// minify and bundle stuff
gulp.task('bundle', ['clean'], function() {
  gulp.src('./bundle.config.js')
    .pipe(bundle())
    .pipe(bundle.results({
      dest: '.', // destination of the bundle.result.json
      pathPrefix: '/' // prefix for each path in bundle.result.json
    }))
    .pipe(gulp.dest(publicRoot)); // destination of the bundle files
});

// watch for changes
gulp.task('watch', ['bundle'], function() {
  gulp.watch(
    [
      'Sections/**/Content/less/*.less',
      'Sections/**/Content/css/*.css',
      'Sections/**/Content/fonts/*.*',
      'Sections/**/Content/img/*.*',
      'Sections/**/Content/js/*.js',
      'Sections/**/Content/js/**/*.js'
    ],
    ['bundle']
  );
});

// do stuff and then start watching to to stuff, when it is needed again
gulp.task('default', ['watch']);

