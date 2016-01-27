// Less configuration
//var fs = require('fs');
//var path = require('path');
//var merge = require('merge-stream');
//var uglify = require('gulp-uglify');
//var sourcemaps = require('gulp-sourcemaps');
var gulp = require('gulp');
var bundle = require('gulp-bundle-assets');
var rimraf = require('gulp-rimraf');
//var concat = require('gulp-concat');

var publicRoot = "./wwwroot";
var dirMaps = publicRoot + "/maps"

var scriptsSrc = 'Content/js';
var scriptsDst = publicRoot;

// function getFolders(dir) {
//   return fs.readdirSync(dir)
//     .filter(function(file) {
//       return fs.statSync(path.join(dir, file)).isDirectory();
//     });
// }
// function getVersion() {
//   var d = new Date();
//   return d.getTime();
// }
// gulp.task('bundle2', function() {
//   var ver = getVersion();
//   var folders = getFolders(scriptsSrc);
// 
//   var tasks = folders.map(function(folder) {
//     // concat into foldername.js
//     // minify
//     // write to output again
//     return gulp.src(path.join(scriptsSrc, folder, '/**/*.js'))
//       .pipe(sourcemaps.init())
//       .pipe(concat(folder + " - " + ver + '.js'))
//       .pipe(uglify())
//       .pipe(sourcemaps.write(dirMaps))
//       .pipe(gulp.dest(scriptsDst));
//   });
// 
//   // process all remaining files in scriptsPath root into main.js and main.min.js files
//   var root = gulp.src(path.join(scriptsSrc, '/*.js'))
//       .pipe(sourcemaps.init())
//       .pipe(concat('main-' + ver + '.js'))
//       .pipe(uglify())
//       .pipe(sourcemaps.write(dirMaps))
//       .pipe(gulp.dest(scriptsDst));
// 
//    // write something to a file
//   gulp.task('taskname', function(cb){
//     fs.writeFile('filename.txt', 'Blahblah', cb);
//   });
// 
//   return merge(tasks, root);
// });

// clean up destination directory before bundling and copying new stuff
gulp.task('clean', function () {
  return gulp.src(publicRoot+'/**/*.{js,css,map}', { read: false })
    .pipe(rimraf());
});

// minify and bundle stuff
gulp.task('bundle', ['clean'], function() {
  gulp.src('./Content/bundle.config.js')
    .pipe(bundle())
    .pipe(bundle.results({
      dest: './Content', // destination of the bundle.result.json
      pathPrefix: '/' // prefix for each path in bundle.result.json
    }))
    .pipe(gulp.dest(publicRoot)); // destination of the bundle files
});

// watch for changes
gulp.task('watch', ['bundle'], function() {
  gulp.watch(
    [
      'Content/less/*.less',
      'Content/css/*.css',
      'Content/fonts/*.*',
      'Content/img/*.*',
      'Content/js/*.js',
      'Content/js/**/*.js'
    ],
    ['bundle']
  );
});

// do stuff and then start watching to to stuff, when it is needed again
gulp.task('default', ['watch']);

