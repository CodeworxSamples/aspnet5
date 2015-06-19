interface IBookScope extends ng.IScope {
    book: IBook;

    ok: Function;
    cancel: Function;
}

class BookController {
    constructor(private $scope: IBookScope, private $http: ng.IHttpService,
        private $modalInstance: angular.ui.bootstrap.IModalServiceInstance, private bookId: string) {
        // Load requested book
        this.loadBook();

        this.$scope.ok = () => {
            // Save changes to book
            this.$http
                .put("/api/book", this.$scope.book)
				.success(() => $modalInstance.close($scope.book));
		};

		this.$scope.cancel = () => $modalInstance.dismiss("cancel");
	}

	private loadBook() {
        this.$http
            .get("/api/book/" + this.bookId)
			.success((result: IBook) => {
			    if (result) {
				    this.$scope.book = result;
			    } else {
				    alert("Book " + this.bookId + " not found.");
			    }
            })
            .error(() => alert("Book " + this.bookId + " could not be loaded."));
	}
}

angular.module("angularApp").controller("BookController", BookController);