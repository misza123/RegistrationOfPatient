const ExtractTextPlugin = require('extract-text-webpack-plugin');

module.exports = {
    output: {
        filename: 'app.js'
    },
    module: {
        loaders: [{
            test: /\.js$/,
            loader: 'babel-loader',
            exclude: /node_modules/
        }, {
            test: /\.html$/,
            loader: 'html-loader',
            exclude: /node_modules/
        }, {
            test: /\.(scss|sass)$/,
            use: ExtractTextPlugin.extract({
                fallback: 'style-loader',
                use: ['css-loader', 'postcss-loader', 'sass-loader']
            })
        }, {
            test: /\.(ttf|otf|eot|svg|woff(2)?)(\?[a-z0-9]+)?$/,
            loader: 'url-loader'
        }, {
            test: /\.(png|jpg)$/,
            loader: 'file-loader'
        }]
    },
    plugins: [
        new ExtractTextPlugin('app.css')
    ]
};
