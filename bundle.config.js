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
    common: {
      scripts: [
        './Sections/Common/Content/js/*.js'
      ],
      styles: [
        './Sections/Common/Content/less/main.less'
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
        './Sections/Common/Content/css/themes/**/*.css'
      ],
      options: defOpts
    },
    start: {
      scripts: [
        './Sections/Start/Content/js/*.js'
      ],
      styles: [],
      options: defOpts
    },
    news: {
      scripts: [
        './Sections/News/Content/js/*.js'
      ],
      styles: [],
      options: defOpts
    },
    demos: {
      scripts: [
        './Sections/Demos/Content/js/*.js'
      ],
      styles: [],
      options: defOpts
    },
    about: {
      scripts: [
        './Sections/About/Content/js/*.js'
      ],
      styles: [],
      options: defOpts
    }
  },
  copy: [
      {
        src: './Sections/**/Content/img/*.{png,jpg}',
        base: './Sections/**/Content/',
        watch: true
      },
      {
        src: './Sections/**/Content/fonts/*.*',
        base: './Sections/**1/Content/',
        watch: true
      }
    ]
};
