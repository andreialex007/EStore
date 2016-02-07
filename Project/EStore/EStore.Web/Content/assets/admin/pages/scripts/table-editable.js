var TableEditable = function () {

    
    var handleTable = function () {

        function restoreRow(oTable, nRow) {
            var aData = oTable.fnGetData(nRow);
            oTable.fnUpdate(aData.Role, nRow, 0, false);
            oTable.fnUpdate(aData.Title, nRow, 1, false);
            oTable.fnUpdate(aData.FirstName, nRow, 2, false);
            oTable.fnUpdate(aData.LastName, nRow, 3, false);
            oTable.fnUpdate(aData.Email, nRow, 4, false);
            oTable.fnUpdate(aData.Phone, nRow, 5, false);
            oTable.fnUpdate(aData.Mobile, nRow, 6, false);
            oTable.fnUpdate('&nbsp;&nbsp;&nbsp;<a class="edit btn default btn-xs blue"  href=""><i class="fa fa-edit"></i></a><a class="delete btn default btn-xs red" style="" href=""><i class="fa-trash-o"></i></a>', nRow, 7, false);
            oTable.fnDraw();
            nDoubleClick = false;
        }

        function editRow(oTable, nRow) {
            var aData = oTable.fnGetData(nRow);
            var jqTds = $('>td', nRow);
            jqTds[0].innerHTML = '<input id="Role" type="text" class="form-control input-sm" value="' + aData.Role + '">';
            jqTds[1].innerHTML = '<input id="Title" type="text" class="form-control input-sm" value="' + aData.Title + '">';
            jqTds[2].innerHTML = '<input id="FirstName" type="text" class="form-control input-sm" value="' + aData.FirstName + '">';
            jqTds[3].innerHTML = '<input id="LastName" type="text" class="form-control input-sm" value="' + aData.LastName + '">';
            jqTds[4].innerHTML = '<input id="Email" type="text" class="form-control input-sm" value="' + aData.Email + '">';
            jqTds[5].innerHTML = '<input id="Phone" type="text" class="form-control input-sm" value="' + aData.Phone + '">';
            jqTds[6].innerHTML = '<input id="Mobile" type="text" class="form-control input-sm" value="' + aData.Mobile + '">';
            jqTds[7].innerHTML = '&nbsp;&nbsp;&nbsp;<a id="saveref" class="edit btn default btn-xs green" href=""><i class="fa fa-save"></i></a><a id="cancelref" class="cancel btn default btn-xs red" href=""><i class="glyphicon glyphicon-remove"></i></a>';
            //jqTds[8].innerHTML = '&nbsp;&nbsp;<a id="cancelref" class="cancel btn default btn-xs red" href=""><i class="glyphicon glyphicon-remove"></i></a>';

            $('#Role').focus();
            nDoubleClick = true;
            var x = document.getElementById("saveref");
            if (x != null)
                x.addEventListener("focus", function () { saveRow(oTable, nRow); }, true);
        }

        function saveRow(oTable, nRow) {
            debugger;
            var aData = oTable.fnGetData(nRow);
            var jqInputs = $('input', nRow);

            oTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
            oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
            oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
            oTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
            oTable.fnUpdate(jqInputs[4].value, nRow, 4, false);
            oTable.fnUpdate(jqInputs[5].value, nRow, 5, false);
            oTable.fnUpdate(jqInputs[6].value, nRow, 6, false);

            $.ajax({
                url: '/Customers/SaveContact',
                type: "POST",
                dataType: "json",
                data: { Role: jqInputs[0].value, Title: jqInputs[1].value, FirstName: jqInputs[2].value, LastName: jqInputs[3].value, Email: jqInputs[4].value, Phone: jqInputs[5].value, Mobile: jqInputs[6].value, CustomerId: document.getElementById('PKID').value, PKID: aData.PKID },
                success: function (result) {

                    if (result) {
                        aData.PKID = result;
                        UIToastr.ShowMessage('success', 'Success', 'Contact saved successfully.');
                        oTable.fnUpdate('&nbsp;&nbsp;&nbsp;<a class="edit btn default btn-xs blue" href=""><i class="fa fa-edit"></i></a><a class="delete btn default btn-xs red" href=""><i class="fa fa-trash-o"></i></a>', nRow, 7, false);
                        oTable.fnDraw();
                        $('#cancelref').focus();

                        //nEditing = null;
                        nNew = false;
                    } else
                        UIToastr.ShowMessage('error', 'Error', 'Error occurred while updating the contact.');
                },
                error: function (result) {
                    UIToastr.ShowMessage('error', 'Error', 'Error occurred while updating the contact.');
                }

            });
            nDoubleClick = false;

        }

        function deleteRow(oTable, nRow) {

            var aData = oTable.fnGetData(nRow);

            $.ajax({
                type: "POST",
                contentType: "application/json;charset=utf-8",
                url: "/Customers/DeleteContact",
                data: '{"id":"' + aData.PKID + '"}',
                dataType: "json",
                success: function (value) {
                    if (value) {
                        oTable.fnDeleteRow(nRow);
                        UIToastr.ShowMessage('success', 'Success', 'Contact deleted successfully.');
                    }

                    else
                        UIToastr.ShowMessage('error', 'Error', 'Error occurred while deleting the contact.');
                },
                error: function () {
                    UIToastr.ShowMessage('error', 'Error', 'Error occurred while deleting the contact.');
                }

            });


        }

        function cancelEditRow(oTable, nRow) {
            var jqInputs = $('input', nRow);
            oTable.fnUpdate(jqInputs[0].value, nRow, 0, false);
            oTable.fnUpdate(jqInputs[1].value, nRow, 1, false);
            oTable.fnUpdate(jqInputs[2].value, nRow, 2, false);
            oTable.fnUpdate(jqInputs[3].value, nRow, 3, false);
            oTable.fnUpdate('<a class="edit" href="">Edit</a>', nRow, 4, false);
            oTable.fnDraw();
        }

        var table = $('#sample_editable_1');

        var oTable = table.dataTable({
            "lengthMenu": [
                [5, 15, 20, -1],
                [5, 15, 20, "All"]
            ],

            "pageLength": 15,
            "language": {
                "lengthMenu": " _MENU_ records"
            },
            "columnDefs": [
                {
                    // set default column settings
                    'orderable': true,
                    'targets': [0]
                }, {
                    "searchable": true,
                    "targets": [0]
                },
                { "data": "Role", "targets": [0] },
                { "data": "Title", "targets": [1] },
                { "data": "FirstName", "targets": [2] },
                { "data": "LastName", "targets": [3] },
                { "data": "Email", "targets": [4] },
                { "data": "Phone", "targets": [5] },
                { "data": "Mobile", "targets": [6] },
                { "data": "PKID" },
                {
                    "targets": -1,
                    "data": null,
                    "defaultContent": '&nbsp;&nbsp;&nbsp;<a class="edit btn default btn-xs blue"  href=""><i class="fa fa-edit"></i></a><a class="delete btn default btn-xs red" href=""><i class="fa fa-trash-o"></i></a>'
                }
            ],
            "ajax": {
                "url": '/Customers/GetContacts?id=' + document.getElementById('PKID').value + '',
            },
            "order": [
                [0, "asc"]
            ] 
        });

        var tableWrapper = $("#sample_editable_1_wrapper");

        tableWrapper.find(".dataTables_length select").select2({
            showSearchInput: false 
        }); 

        var nEditing = null;
        var nNew = false;
        var nDoubleClick = false;

        $('#sample_editable_1_new').click(function (e) {
            e.preventDefault();
            debugger;

            if (nNew && nEditing) {
                if (confirm("Previous row not saved. Do you want to save it ?")) {
                    saveRow(oTable, nEditing); // save
                    $(nEditing).find("td:first").html("Untitled");
                    nEditing = null;
                    nNew = false;

                } else {
                    oTable.fnDeleteRow(nEditing); // cancel
                    nEditing = null;
                    nNew = false;

                    return;
                }
            }

            var aiNew = oTable.fnAddData([{ Role: '', Title: '', FirstName: '', LastName: '', Email: '', Phone: '', Mobile: '' }]);
            var nRow = oTable.fnGetNodes(aiNew[0]);
            editRow(oTable, nRow);
            nEditing = nRow;
            nNew = true;
        });

        table.on('click', '.delete', function (e) {
            e.preventDefault();
            var curr = this;
            dialogsApi.showConfirmModal("Confirm deletion?", "Are you sure to delete this row ?", function () {
                var nRow = $(curr).parents('tr')[0];
                deleteRow(oTable, nRow);
            });
        });

        table.on('click', '.cancel', function (e) {
            e.preventDefault();
            if (nNew) {
                oTable.fnDeleteRow(nEditing);
                nEditing = null;
                nNew = false;
            } else {
                restoreRow(oTable, nEditing);
                nEditing = null;
            }
        });

        table.on('click', '.edit', function (e) {
            e.preventDefault();

            /* Get the row as a parent of the link that was clicked on */
            var nRow = $(this).parents('tr')[0];

            if (nEditing !== null && nEditing != nRow) {
                /* Currently editing - but not this row - restore the old before continuing to edit mode */
                restoreRow(oTable, nEditing);
                editRow(oTable, nRow);
                nEditing = nRow;
            } else if (nEditing == nRow && this.innerHTML == "Save") {
                /* Editing this row and want to save it */
                saveRow(oTable, nEditing);
                nEditing = null;
                nNew = false;

            } else {
                /* No edit in progress - let's start one */
                editRow(oTable, nRow);
                nEditing = nRow;
            }
        });

        table.on('keyup', function (e) {
            e.preventDefault();

            if (e.keyCode == 27) {
                if (nNew) {
                    oTable.fnDeleteRow(nEditing);
                    nEditing = null;
                    nNew = false;
                } else {
                    restoreRow(oTable, nEditing);
                    nEditing = null;
                }
            }
        });

        table.on('dblclick', function (e) {
            e.preventDefault();
            if (nDoubleClick == false) {
                var nRow = e.target.parentNode;

                if (nEditing !== null && nEditing != nRow) {
                    /* Currently editing - but not this row - restore the old before continuing to edit mode */
                    restoreRow(oTable, nEditing);
                    editRow(oTable, nRow);
                    nEditing = nRow;
                }
                else {
                    /* No edit in progress - let's start one */
                    editRow(oTable, nRow);
                    nEditing = nRow;
                }
            }

        });
    }

    return {

        //main function to initiate the module
        init: function () {
            handleTable();
        }

    };

}();