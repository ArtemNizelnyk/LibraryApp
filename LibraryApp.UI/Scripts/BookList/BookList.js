$(function () {
    $("#grid").jqGrid({
        url: "/Books/GetBooks",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['BookId', 'Title', 'IsAvailable', 'AuthorNames'], 
        colModel: [
            { key: true, hidden: true, name: 'BookId', index: 'BookId', editable: false },
            { key: false, name: 'Name', index: 'Name', editable: false, sorttype: "Name", search: false },
            { key: false, name: 'IsAvailable', index: 'IsAvailable', editable: false, stype: "select", searchoptions: { sopt: ['eq'], value: ":All;true:Available;false:Taken" } },
            { key: false, name: 'AuthorNames', index: 'AuthorNames', editable: false, search: false }],
        pager: jQuery('#pager'),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        height: '100%',
        viewrecords: true,
        caption: 'Book List',
        loadonce: true, // полная загрузка для уменьшения запросов к базе
        sortable: true,
        loadComplete: function () {
            $("tr.jqgrow", this).contextMenu('myMenu1', {
                bindings: {
                    'take': function (trigger) {
                        var rowId = $(trigger).closest("tr.jqgrow").attr("id");
                        var bookId = $('#grid').jqGrid('getCell', rowId, 'BookId');
                        window.location.href = '/Order/MakeOrder/' + bookId;

                    },
                    'return': function (trigger) {
                        var rowId = $(trigger).closest("tr.jqgrow").attr("id");
                        var bookId = $('#grid').jqGrid('getCell', rowId, 'BookId');
                        window.location.href = '/Order/ReturnBook/' + bookId;
                    }
                },
                onContextMenu: function (event/*, menu*/) {
                    var rowId = $(event.target).closest("tr.jqgrow").attr("id");
                    var avbl = $('#grid').jqGrid('getCell', rowId, 'IsAvailable');

                    //grid.setSelection(rowId);
                    // Убираем возможность вернуть доступные книги и взять недоступные
                    $('#return').attr("disabled", avbl == "true");
                    $('#take').attr("disabled", avbl == "false");
                    if (avbl == "true") {
                        $('#return').attr("disabled", "disabled").addClass('ui-state-disabled');
                        $('#take').removeAttr("disabled").removeClass('ui-state-disabled');
                    } else {
                        $('#take').attr("disabled", "disabled").addClass('ui-state-disabled');
                        $('#return').removeAttr("disabled").removeClass('ui-state-disabled');
                    }
                    return true;
                }
            });
        },
        gridview: true,
        ignoreCase: true,
        emptyrecords: 'No records to display',
        jsonReader: {
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "BookId"
        },
        autowidth: true,
        multiselect: false
    }).navGrid('#pager', { edit: false, add: false, del: false, search: true }),

    $('#grid').jqGrid('filterToolbar', {
        // JSON stringify all data from search, including search toolbar operators
        stringResult: true,
        // instuct the grid toolbar to show the search options
        searchOperators: true
    }),

    $("#grid").contextMenu({
        menu: "myMenu1"
    });

    

});