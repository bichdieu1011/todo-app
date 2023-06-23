const webpack = require('webpack');
require('dotenv').config({ path: './.env' });

function getClientEnvironment() {
  // Grab NX_* environment variables and prepare them to be injected
  // into the application via DefinePlugin in webpack configuration.
  const NX_APP = /^TD_/i;
  const raw = Object.keys(process.env)
    .filter((key) => NX_APP.test(key))
    .reduce((env, key) => {
      env[key] = process.env[key];
      return env;
    }, {});


  // Stringify all values so we can feed into webpack DefinePlugin
  var res = {
    'process.env': Object.keys(raw).reduce((env, key) => {
      env[key] = JSON.stringify(raw[key]);
      return env;
    }, {}),
  };

  return res;
}

function getClientEnvironmentDev() {

  // Stringify all values so we can feed into webpack DefinePlugin
  var res = {
    "process.env": JSON.stringify(process.env),
  };

  return res;

}

module.exports = (config, options, context) => {
  // Overwrite the mode set by Angular if the NODE_ENV is set
  config.mode = process.env.NODE_ENV || config.mode;
  if (config.mode == "production") {
    config.plugins.push(new webpack.DefinePlugin(getClientEnvironment()));
  }
  else {
    config.plugins.push(new webpack.DefinePlugin(getClientEnvironmentDev()));
  }

  return config;
};