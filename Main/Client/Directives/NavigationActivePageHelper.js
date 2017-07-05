app.directive('navHelper', ['$location', function ($location) {
    return {
        restrict: 'A',
        scope: false,
        link: function (scope, element) {
            function setActive() {
                var path = $location.path();
                if (path.match(/\/products\/\d+$/g) !== null) {
                    angular.forEach(element.find('li'), function (li) {
                        angular.element(li).removeClass('active');
                    });
                    return;
                }
                path = (path == '/') ? '/' : '/products';

                if (path) {
                    angular.forEach(element.find('li'), function (li) {
                        var anchor = li.querySelector('a');
                        //console.log(anchor.href);
                        if (anchor.href.match('#!' + path + '(?=\\?|$)')) {
                            angular.element(li).addClass('active');
                        } else {
                            angular.element(li).removeClass('active');
                        }
                    });
                }
            }
            setActive();
            scope.$on('$locationChangeSuccess', setActive);
        }
    }
}]);