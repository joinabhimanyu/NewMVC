(function () {
    'use strict';

    angular.module('MaterialService', [])

        .service('mtService', function ($http) {

            this.getSearchResult = function (search_term) {
                var request = $http({
                    method: 'GET',
                    url: 'https://api.flickr.com/services/rest',
                    params: {
                        method: 'flickr.photos.search',
                        api_key: '13fc3d8b814a9ac551e7a136b0ce6ae9',
                        text: search_term,
                        format: 'json',
                        nojsoncallback: 1
                    }
                });
                return request;    
            };

        });

})();