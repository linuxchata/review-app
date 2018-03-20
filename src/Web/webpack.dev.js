const merge = require('webpack-merge');
const common = require('./webpack.common.js');

module.exports = merge(common, {
  module: {
    rules: [
      {
        test: /\.(scss)$/,
        use: [
          {
            loader: "style-loader"
          },
          {
            loader: "css-loader"
          },
          {
            loader: "sass-loader"
          }],
        exclude: /node_modules/
      }
    ]
  },
  devtool: 'inline-source-map',
  devServer: {
    contentBase: './dist'
  }
});