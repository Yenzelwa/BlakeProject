
angular.module('App');

app.controller('PersonCntrl', function ($scope, $http) {
    $scope.Persons = [];
    $scope.Accounts = {};
    $scope.Transactions = [];
    $scope.CurrentPerson = {};
    $scope.CurrentPersonSelected = 0;
    $scope.SaveStatus = "";
    $scope.CurrentPage = 1;
    $scope.TotalPages = 0;
    $scope.PageSize = 15;
   
    $scope.GetPerson = function (pesonId) {
        $scope.showPersonDetail = true;
        $scope.CurrentPerson = {};
        $http({
            url: '/Person/GetPersonById/',
            method: "GET",
            params: { personId: pesonId },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (response) {
            $scope.CurrentPerson = response.data;
            $scope.CurrentPersonSelected = pesonId;

            
        });
    }

    $scope.showPersonDetail = false;
    $scope.hidePersonDetails = function () {
    $scope.showPersonDetail = true;

    }


    $scope.SavePerson = function (person) {
        debugger;
        $http({
            url: '/Person/SavePerson/',
            method: "POST",
            data: { person: person },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (response) {
            $scope.CurrentPerson = response.data;
            $scope.CurrentPersonSelected = 1;
            $scope.SaveStatus = response.data.statusMsg;
            $scope.Navigate();
        
        }
        );
    }

    //method called to retrieve grid data
    $scope.EditAccount = function (id) {
        personId = $scope.CurrentPersonSelected;
        $http.post('/Person/AccountDetail', { accId: id, personId: personId}).then(function (ul) {
            window.location.href = ul.data.redirectUrl;
        });
    }

    $scope.Navigate = function () {
        var dataToSend = {
            currentPage: $scope.CurrentPage,
            pageSize: $scope.PageSize,
            filterLetter: $scope.searchKeyword
        };
        $http.post('/Person/GetPersonList', dataToSend).then(function (ul) {
            $scope.TotalPages = ul.data[0].TotalPages;
            $scope.Persons = ul.data;

        });
    }

    ///======== NEXT PAGE =============



    $scope.searchKeyword = "";

    //Dispatch the next page arrow nav for grid to the appropriete function
    $scope.NextPageListing = function () {
        if ($scope.CurrentPage < $scope.TotalPages) {
            var pageValue = $scope.CurrentPage + 1;
            if (pageValue <= $scope.TotalPages) {
                $scope.CurrentPage = pageValue;
                $scope.Navigate();
            }
        }
    }

    //prev page arrow nav for grid
    $scope.PrevPageListing = function () {
        if ($scope.CurrentPage > 1) {
            var pageValue = $scope.CurrentPage;
            $scope.CurrentPage = pageValue - 1;
            $scope.Navigate();
        }
    }

    ///======== END PRIV PAGE =============

    ///======== FIRST PAGE =============


    //go to first page
    $scope.FirstPageListing = function () {
        $scope.CurrentPage = 1;
        $scope.Navigate();
    }

    ///======== END FIRST PAGE =============


    //go to last page
    $scope.LastPageListing = function () {
        var lastPageNo = $scope.TotalPages;
        if (lastPageNo > 0) {
            $scope.CurrentPage = lastPageNo;
            $scope.Navigate();
        }
    }

    //default grid items on first page
    $scope.Navigate();


   
    $scope.DeletePerson = function (personId) {
        debugger;
        $http({
            url: '/Person/DeletePerson/',
            method: "POST",
            data: { personId: personId },
            headers: { 'Content-Type': 'application/json' }
        }).then(function (response) {
            $scope.CurrentAccount = response.data;
            $scope.SaveStatus = response.data.statusMsg;  
        });
    }

});