interface IHomeScope extends ng.IScope {
    books: IBook[];
    gridOptions: any;

    editBook: (id: string) => void;
}

class HomeController {
	constructor(private $scope: IHomeScope, private $http: ng.IHttpService, private $modal: angular.ui.bootstrap.IModalService) {
		this.$scope.books = [];
		this.$scope.editBook = bookId => this.editBook(bookId);

		this.initializeGrid();
		this.loadBooks();
	}

	private initializeGrid() {
		this.$scope.gridOptions = {
			data: "books",
			enableRowSelection: true,
			enableGridMenu: true,
			enableSorting: true,
			columnDefs: [
				{ name: "title", type: "string" },
				{ name: "price", type: "number" },
                {
                    field: "id",
                    displayName: "",
                    enableCellEdit: false,
                    cellTemplate: "<div class=\"ui-grid-cell-contents\" ng-class=\"col.colIndex()\"><a ng-click=\"grid.appScope.editBook(COL_FIELD)\">Edit</a></div>"
                }
			]
		};
	}

	private loadBooks() {
		this.$http.get<IBook[]>("/api/book")
			.success(result => this.$scope.books = result);
	}

	private editBook(id: string) {
        this.$modal
            .open({
			    animation: true,
			    templateUrl: "bookModalContent.html",
			    controller: "BookController",
			    resolve: {
				    bookId: () => id
			    }
		    })
            .result.then(() => this.loadBooks());
	}
}

angular.module("angularApp").controller("HomeController", ["$scope", "$http", "$modal", HomeController]);