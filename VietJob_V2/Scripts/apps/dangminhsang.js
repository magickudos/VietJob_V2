var app = angular.module("myApp", []);

// Controller
app.controller("myController", function ($scope) {

});

// Services
app.service("myServices", function($http) {
    this.GetMatching = function (url, _cv, _jd, successFunction, finishedFunction) {
        Get_AjaxCaller(url, {cv: _cv, jd: _jd}, successFunction, finishedFunction);
    }
});

// Filter
app.filter("unsafe", function ($sce) { return $sce.trustAsHtml; });

app.filter('aspDate', function () {
    'use strict';

    return function (input) {

        if (input) {
            return parseInt(input.substr(6));
        }
        else {
            return;
        }
    };
});

// Ajax request
function Post_AjaxCaller(url, data, successFunction, finishedFunction) {
    $.when(
        $.ajax({
            url: url,
            type: "POST",
            data: data,
            success: function (result) {
                AjaxErrorHandle(result);
                successFunction(result);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                AjaxFailed(xhr.responseText);
            }
        })
    ).then(finishedFunction);
}

function Get_AjaxCaller(url, data, successFunction, finishedFunction) {
    $.when(
        $.ajax({
            url: url,
            type: "GET",
            data: data,
            success: function (result) {
                successFunction(result);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                AjaxFailed(xhr.responseText);
            }
        })
    ).then(finishedFunction);
}

// Common functions
function AjaxErrorHandle(result) {
    if (result.length === 0) {
        alert("The information was updated successfully");
    } else {
        for (var i = 0; i < result.length; i++) {
            alert(result[i]);
        }
    }
};

function AjaxFailed(error) {
    alert(error);
}