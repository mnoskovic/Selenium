'use strict';

var { After, AfterAll } = require('cucumber');
var webdriver = require('selenium-webdriver');
const { setWorldConstructor, setDefaultTimeout  } = require("cucumber");

var capabilities = {
    browserName: 'chrome',
    chromeOptions: {
        args: ['--start-maximized', '--disable-notifications']
    }
};

function CustomWorld() {
    var config_file = '../../setup/config/' + (process.env.CONFIG_FILE || 'selenium') + '.config.js';
    var config = require(config_file).config;

    this.driver = new webdriver
        .Builder()
        .usingServer(config.server)
        .withCapabilities(capabilities)
        .build();
}

setWorldConstructor(CustomWorld);
setDefaultTimeout(60 * 1000);

After(function () {
    return this.driver.quit();
});


AfterAll(function () {

});
