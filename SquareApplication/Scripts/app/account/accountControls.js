(function() {
    "use strict";

    angular.module("app-square", [])
        .controller("accountControls", accountControls);

    function accountControls($scope) {
        $scope.formValid = false;
        $scope.login = function(valid) {
            
        }

    }

})();