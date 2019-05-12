
'use strict';

var cucumber = require('cucumber');
var webdriver = require('selenium-webdriver'),
    By = webdriver.By,
    until = webdriver.until;

var defaultTimeout = 18000;

var endpointConfigFile = './endpoint.config.js';
if (process && process.env && process.env.ENDPOINT_CONFIG_FILE) {
    endpointConfigFile = './' + process.env.ENDPOINT_CONFIG_FILE;
}

var endpoint = require(endpointConfigFile).endpointConfig;

module.exports = function () {

    this.When(/^I navigate to site$/, function () {
        return this.driver.get(endpoint.url);
    });

    this.Then(/^I search for "([^"]*)"$/, function (phrase) {
        this.driver.wait(until.elementLocated(By.name("q")), defaultTimeout).sendKeys(phrase + '\n');
    });

    this.Then(/^I should see on title "([^"]*)"$/, function (phrase) {
        return this.driver.wait(until.titleContains(phrase), defaultTimeout);
    });


};
