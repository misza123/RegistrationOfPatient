const gulp = require('gulp');
const gulpLoadPlugins = require('gulp-load-plugins');
const webpack = require('webpack');

const webpackConfig = require('./webpack.config');
const webpackProductionConfig = require('./webpack.production.config');

const ENVS = {
    PRODUCTION: 'production',
    DEVELOPMENT: 'development'
};

const $ = gulpLoadPlugins();

gulp.task('default', ['help']);

gulp.task('help', $.taskListing);

gulp.task('set-development-env', () => {
    return process.env.NODE_ENV = ENVS.DEVELOPMENT;
})

gulp.task('set-production-env', () => {
    return process.env.NODE_ENV = ENVS.PRODUCTION;
})

gulp.task('serve', () => {
    return $.runSequence(
        'build-copy-index',
        'build-webpack',
        'serve-express',
        'serve-watch'
    );
});

gulp.task('serve-express', () => {
    return $.express.run(['server/app.js']);
});

gulp.task('serve-watch', () => {
    gulp.watch(['src/**/*.js', 'src/**/*.scss', 'src/**/*.html'], ['build']);
    gulp.watch(['.tmp/**/*'], $.express.notify);
    gulp.watch(['server/app.js'], [$.express.run]);
});

gulp.task('build', () => {
    return $.runSequence(
        'set-development-env',
        'build-copy-index',
        'build-webpack'
    );
});

gulp.task('test', () => {
    return gulp.src(['test/**/*.spec.js'])
        .pipe($.mocha({
            compilers: ['js:babel-core/register']
        }));
});

gulp.task('build-copy-index', () => {
    return gulp.src(['src/index.html'])
        .pipe(gulp.dest('.tmp'));
});

gulp.task('build-webpack', () => {
    if (process.env.NODE_ENV === ENVS.PRODUCTION)
        return gulp.src(['src/app.js'])
            .pipe($.webpack(webpackProductionConfig, webpack))
            .pipe(gulp.dest('.tmp/public'));
    return gulp.src(['src/app.js'])
        .pipe($.webpack(webpackConfig, webpack))
        .pipe(gulp.dest('.tmp/public'));

});

gulp.task('build-inline-html', () => {
    return gulp.src('.tmp/index.html')
        .pipe($.inline({
            base: '.tmp/',
            js: $.uglify,
            css: $.cssmin,
            png: $.inlineImageHtml,
            jpg: $.inlineImageHtml
        }))
        .pipe(gulp.dest(__dirname + '/../WebRegistrationOfPatient/wwwroot/app'));
});

gulp.task('package', () => {
    return $.runSequence(
        'set-production-env',
        'build-copy-index',
        'build-webpack',
        'build-inline-html'
    );
});