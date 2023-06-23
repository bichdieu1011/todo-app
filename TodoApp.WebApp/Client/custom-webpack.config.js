
const webpack = require("webpack");
exports.default = {
  plugins: [
    new webpack.DefinePlugin({
        'process.env.baseUrl': process.env.baseUrl
    }),
  ],
};

