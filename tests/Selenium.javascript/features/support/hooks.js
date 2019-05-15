'use strict';

var path = require('path');

var webdriver = require('selenium-webdriver');

var config_file = '../../setup/config/' + (process.env.CONFIG_FILE || 'selenium') + '.config.js';
var config = require(config_file).config;

var createSession = function (config, caps) {

    var driver = new webdriver
        .Builder()
        .usingServer('http://' + config.server + '/wd/hub')
        .withCapabilities(caps)
        .build();

    return driver;
};

var myHooks = function () {
   
    this.Before(function (scenario, callback) {

        var world = this;
        
        var capability = {
            browserName: 'chrome',
             chromeOptions: {
                 args: ['--start-maximized', '--disable-notifications'] 
            }
        };

        var featureName = path.basename(scenario.getUri());

        capability['name'] = featureName + ' -> ' + scenario.getName();

        world.driver = createSession(config, capability);
        callback();
    });

    this.After(function (scenario, callback) {
        this.driver.quit().then(function () {
            callback();
        });
    });
};

module.exports = myHooks;