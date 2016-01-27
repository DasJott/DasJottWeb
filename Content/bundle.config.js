// bundle.config.js

var less = require('gulp-less');

var defOpts = {
  useMin: false, // {(boolean|string|Array)} pre-minified files from bower
  uglify: true, // {(boolean|string|Array)} js minification
  minCSS: true, // {(boolean|string|Array)} css minification
  rev: true // {(boolean|string|Array)} file revisioning
}

module.exports = {
  bundle: {
    base: {
      scripts: [
        './Content/js/jquery-2.2.0.js',
        './Content/js/jquery-ui-1.11.4.js',
        './Content/js/bootstrap.js',
        './Content/js/bootbox.js',
        './Content/js/chosen.jquery.js',
        './Content/js/LoadSpinner.js',
        './Content/js/Utils.js',
        './Content/js/main.js'
      ],
      styles: [
        './Content/less/main.less'
      ],
      options: {
        useMin: false, // {(boolean|string|Array)} pre-minified files from bower
        uglify: true, // {(boolean|string|Array)} js minification
        minCSS: true, // {(boolean|string|Array)} css minification
        rev: true, // {(boolean|string|Array)} file revisioning
        transforms: {
          styles: less
        },
        order: {
          scripts: [
            '**/jquery-*.js',
            '**/bootstrap*.js',
            '!**/js/main.js',
            '**/js/main.js'
          ]
        }
      }
    },
    jquery_ui: {
      styles: [
        './Content/css/themes/**/*.css'
      ],
      options: defOpts
    },
    home: {
      scripts: [
        './Content/js/Home/Index.js'
      ],
      styles: [],
      options: defOpts
    }
  },
  copy: [
      {
        src: './Content/img/*.{png,jpg}',
        base: './Content/',
        watch: true
      },
      {
        src: './Content/fonts/*.*',
        base: './Content/',
        watch: true
      }
    ]
};
