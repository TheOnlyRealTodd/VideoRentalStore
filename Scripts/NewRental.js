                var vm = {
                    movieIds: []
                };
                var customers = new Bloodhound({
                    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
                    queryTokenizer: Bloodhound.tokenizers.whitespace,
                    remote: {
                        url: '/api/CustomersApi?query=%QUERY',
                        wildcard: '%QUERY'
                    }
                });

                $('#customer')
                    .typeahead({
                        minLength: 3,
                        highlight: true
                    },
                    {
                        name: 'customers',
                        display: 'name',
                        source: customers
                    })
                    .on("typeahead:select",
                        function(e, customer) {
                            vm.customerId = customer.id;
                        });


                var movies = new Bloodhound({
                    datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
                    queryTokenizer: Bloodhound.tokenizers.whitespace,
                    remote: {
                        url: '/api/MoviesApi?query=%QUERY',
                        wildcard: '%QUERY'
                    }
                });

                $('#movie')
                    .typeahead({
                        minLength: 3,
                        highlight: true
                    },
                    {
                        name: 'movies',
                        display: 'name',
                        source: movies
                    }).on("typeahead:select", function(e, movie) {
                        $("#movies").append("<li class='list-group-item'>"+movie.name+"</li>");

                        $("#movie").typeahead("val","");

                        vm.movieIds.push(movie.id);
                    });
                $.validator.addMethod("validMovie",
                    function() {
                        return vm.movieIds.length > 0;
                    },
                    "At least 1 movie must be entered.");

                $.validator.addMethod("validCustomer",
                    function() {
                        return vm.customerId && vm.customerId !== 0;
                    }, "Please select a valid customer.");
             var validator =   $("#newRental").validate({
                    submitHandler: function() {
                        $.ajax({
                                url: "/api/newRentals",
                                method: "post",
                                data: vm

                            })
                            .done(function() {
                                toastr.success("The rental has been processed!");
                                $("#customer").typeahead("val","");
                                $("#movie").typeahead("val","");
                                $("#movies").empty();

                                vm ={ movieIds: [] };
                                validator.resetForm();
                            })
                            .fail(function() {
                                toastr.error("Sorry, there was an error.");
                            });
                        return false;
                    }
                });