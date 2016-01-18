var contact = (function (my, $) {
    var constructorSpec = {
        contactsGridAction: '',
        newContactAction: '',
        editContactAction: '',
        deleteContactAction: ''
    };
   
    my.init = function (options) {
        $(function () {
            var self = this;
           
            constructorSpec = options;

            $.ajaxSetup({
                cache: false
            });

            var loadContact = function (id) {
                self.selectedId = id;
                $.get(constructorSpec.editContactAction, { id: id }, function (result) {
                    $("#contactContent").html(result);
                });
            };

            var loadNewContact = function () {
                self.selectedId = null;
                self.dirtyAccount = false;
                $.get(constructorSpec.newContactAction, {}, function (result) {
                    $("#contactContent").html(result);
                });
            };

            var saveContact = function () {
                var dfd = new $.Deferred();

                var form = $("#contactForm");
                $.ajax({
                    url: form.prop('action'),
                    type: form.prop('method'),
                    data: form.serialize()
                }).done(function(result) {
                    if (result.Success) {
                        if (result.Object) {
                            self.selectedId = result.Object;
                        }

                        pageGrids.contactsGrid.refreshPartialGrid();

                        dfd.resolve({ Success: true });
                    } else {
                        $("#contactContent").html(result);
                        dfd.resolve({ Success: false });
                    }
                });

                return dfd.promise();
            };

            var deleteContact = function (id) {
                if (confirm('Are you sure you want to delete this contact record?')) {
                    $.post(constructorSpec.deleteContactAction, { id : id })
                        .done(function(result) {
                            if (result.Success) {
                                // clear selection
                                self.selectedId = null;
                                pageGrids.contactsGrid.refreshPartialGrid().done(function (response) {
                                    if (!response.HasItems) {
                                        $("#Create").click();
                                    } else {
                                        // row is not already loaded
                                        if (self.selectedId == null) {
                                            // load first record in the grid
                                            $("#contactsGridContent .grid-mvc table tbody tr:not(.grid-empty-text):first").click();
                                        }
                                    }
                                });
                            } else {
                                $("#contactStatus").html(result.ErrorMessage);
                            }
                        });
                }
            };

            pageGrids.contactsGrid.ajaxify({
                getPagedData: constructorSpec.contactsGridAction,
                getData: constructorSpec.contactsGridAction,
            });

            pageGrids.contactsGrid.onGridLoaded(function (result) {
                /* keep row selection after sorting */
                var rowToSelect = $("#contactsGridContent .grid-mvc table tbody tr td[data-name='Id']").filter(function () {
                    return $(this).text() == self.selectedId;
                });
                pageGrids.contactsGrid.markRowSelected(rowToSelect.parent());
                if (self.selectedId) {
                    loadContact(self.selectedId);
                }
            });

            pageGrids.contactsGrid.onRowSelect(function (e) {
                self.selectedId = e.row.Id;
                loadContact(self.selectedId);
            });

            /* mark first row selected initially */
            var initialRowToSelect = $('#contactsGridContent .grid-mvc table tbody tr:not(.grid-empty-text):first');
            if (initialRowToSelect.length > 0) {
                self.selectedId = $("#contactsGridContent .grid-mvc table tbody tr:not(.grid-empty-text):first td[data-name='Id']").text();
                pageGrids.contactsGrid.markRowSelected(initialRowToSelect);
            } else {
                loadNewContact();
            }

            $("#Create").on('click', function (e) {
                e.preventDefault();

                loadNewContact();

                self.selectedId = null;

                try { pageGrids.contactsGrid.markRowSelected(null); } catch (e) { } finally { }
            });

            $("body").on('click', '#Save', function (e) {
                e.preventDefault();
                saveContact();
            });

            $("body").on('click', '#Delete', function (e) {
                e.preventDefault();
                deleteContact(self.selectedId);
            });
        });
    };

    return my;
}(contact || {}, jQuery));