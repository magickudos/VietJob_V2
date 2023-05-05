app.controller("contentController", function ($scope, myServices) {
    $scope.GetMatchingData = function (_cv, _jd) {
        myServices.GetMatching($("#content").attr("data-get-matching"), _cv, _jd,function (dataResponse) {
            $scope.matching = dataResponse;
        }, function () {
            $scope.$apply();
            showMatchingChart($scope.matching.softskillKeywords.score,
                $scope.matching.hardskillKeywords.score,
                $scope.matching.otherskillKeywords.score);
        });
    }
});

$(document).ready(function () {
    
});

function analyze () {
    var scope = angular.element($("#content")).scope();
    scope.GetMatchingData($('#cv').val(), $('#jd').val());
    $('#matchContent').css('display', 'block');
}