const gulp = require('gulp');
const gulpLoadPlugins = require('gulp-load-plugins');
const webpack = require('webpack');

const webpackConfig = require('./webpack.config');

const $ = gulpLoadPlugins();

gulp.task('default', ['help']);

gulp.task('help', $.taskListing);

gulp.task('serve', () => {
    $.express.run(['server/app.js']);

    gulp.watch(['src/**/*.js', 'src/**/*.less', 'src/**/*.html'], ['build']);

    gulp.watch(['.tmp/**/*'], $.express.notify);

    gulp.watch(['server/app.js'], [$.express.run]);
});

gulp.task('build', ['build-copy-index', 'build-webpack']);

gulp.task('test', () => {
    return gulp.src(['test/**/*.spec.js'])
        .pipe($.mocha({
            compilers: ['js:babel-core/register']
        }));
});

gulp.task('build-copy-index', () => {
    return gulp.src(['src/index.html'])
        .pipe($.contribCopy())
        .pipe(gulp.dest('.tmp'));
});

gulp.task('build-webpack', () => {
    return gulp.src(['src/app.js'])
        .pipe($.webpack(webpackConfig, webpack))
        .pipe(gulp.dest('.tmp/public'));
});
