<div class="row">
	<div class="col-xs-12">
        <div>
            <button type="button" id="previousMonth" class="btn btn-flat previous">&lt</button>
            <input type="month" id="periodPicker" value="<?php echo $year . '-' .floor($month/10) . ($month%10); ?>">
            <button type="button" id="validMonth" class="btn btn-flat previous">Valid</button>
            <button type="button" id="nextMonth" class="btn btn-flat previous">&gt</button>
        </div>
    </div>
</div>

<div class="row">

    <div id="reportrange" style="background: #fff; cursor: pointer; padding: 5px 10px; border: 1px solid #ccc; width: 100%">
        <i class="fa fa-calendar"></i>&nbsp;
        <span></span> <i class="fa fa-caret-down"></i>
    </div> 

    <div class="col-xs-12" id="">
        <div class="box" id="">
            <div class="box-header">
                <h3 class="box-title"><i class="fa fa-th-list"></i> Tableau Reporting</h3>
            </div>
            <a href="<?php echo base_admin_url('reporting/exportReportingTable/' . $year . '/' . $month); ?>" class="btn btn-sm btn-flat btn-default"><i class="fa fa-fw fa-file-excel-o"></i> Export Excel</a>
            <div class="box-body">
                <div class="row">
                    <div class="col-xs-4">

                        <div>Unité :</div>
                        <select id="UnitSelector" class="form-control">
                                <option value="days" <?php echo $unit === 'days' ? 'selected' : '' ?>>Jours</option>
                                <option value="hours"<?php echo $unit === 'hours' ? 'selected' : '' ?>>Heures</option>
                        </select>
                    </div>
                </div>

                <h2>Prod.</h2>
				<!-- Table -->
                <table id="reporting_table1" class="display compact table table-bordered table-hover">
                    <thead>
                        <tr>
                            <?php foreach($reporting_table['header'] as $element) { echo '<th>' . $element . '</th>';} ?>
                            <th>Total</th>
                        </tr>
                    </thead>
                    <tfoot>
                        <tr>
                            <?php foreach($reporting_table['header'] as $element) { echo '<th>' . $element . '</th>';} ?>
                            <th>0</th>
                        </tr>
                    </tfoot>
                </table>
				
				<h2 style="margin-top: 50px;">Créa.</h2>
				<!-- Table -->
                <table id="reporting_table2" class="display compact table table-bordered table-hover">
                    <thead>
                        <tr>
                            <?php foreach($reporting_table['header'] as $element) { echo '<th>' . $element . '</th>';} ?>
                            <th>Total</th>
                        </tr>
                    </thead>
                    <tfoot>
                        <tr>
                            <?php foreach($reporting_table['header'] as $element) { echo '<th>' . $element . '</th>';} ?>
                            <th>0</th>
                        </tr>
                    </tfoot>
                </table>
				
				<h2 style="margin-top: 50px;">Autres</h2>
				<!-- Table -->
                <table id="reporting_table3" class="display compact table table-bordered table-hover">
                    <thead>
                        <tr>
                            <?php foreach($reporting_table['header'] as $element) { echo '<th>' . $element . '</th>';} ?>
                            <th>Total</th>
                        </tr>
                    </thead>
                    <tfoot>
                        <tr>
                            <?php foreach($reporting_table['header'] as $element) { echo '<th>' . $element . '</th>';} ?>
                            <th>0</th>
                        </tr>
                    </tfoot>
                </table>
				
            </div>
        </div>
    </div>
</div>

<script>
    var currentPage = 'reportingTable';
    var date = <?php echo $monthDate; ?>;
    var userIds = [<?php foreach($usersData as $userData) { echo $userData['userId'] . ','; }?>];
    var userData = {};
    <?php foreach($usersData as $userData) { 
        echo 'userData["' . $userData['userId'] . '"] = {
            name: "' . $userData['firstName'] . ' ' . $userData['lastName'] . '"};
            '; 
    }?>;
</script>

<script>
var unit = "<?php echo $unit ?>";
var columnCount = <?php echo count($reporting_table['header']) ?>;


function displayWithUnit(unit, value) {
        var multiplier = (unit === 'days' ? 60 * 60 * 7 : 60 * 60);
        var displayValue = Math.round(value / multiplier * 100) / 100;
        return displayValue;
    }

<?php 
	for($rt = 1; $rt <= 3; $rt++) {
?>
var tableData<?php echo $rt; ?> = [
    <?php $count = 0;
    
		foreach($reporting_table['body'] as $index => $reporting_row) {
        
			if(intval($reporting_row[0]->reporting_type) != $rt) {
				continue;
			}
			
			if($count == 0)
                $count++;
            else echo ',';
            echo '[';
                $sum = 0;
                foreach($reporting_row as $i => $element) {
                    if($i > 0)
                        echo ',';
					if($i == 0) {
						echo '"'.(empty($element->type_prefix) ? $element->name : $element->type_prefix.' '.$element->name) . '","' . $element->client_title . '"';
					}else{
						echo '"'. $element . '"';
					    $sum += intval($element);
					}
                }
                echo ', '. $sum;
            echo ']';
        
    } ?>
];
	
<?php 
	}
?>
	
	//console.log(tableData);

function setDataTable() {
    $(function() {

    var start = moment().subtract(29, 'days');
    var end = moment();
    var test = false;

    function cb(start, end) {
        $('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
        const date = new Date($('#periodPicker').val());
        const dateMonth = parseInt(start.format('M')) + parseInt(start.format('Y')) * 12 - 1;
        if(test)
            location = '/reporting/' + currentPage + '/' + dateMonth;
        else test = true;
    }

    $('#reportrange').daterangepicker({
        startDate: start,
        endDate: end,
        locale: {
            format: 'MM/YYYY'
        },
        ranges: {
           'Mois actuel': [moment().startOf('month'), moment()],
           'Mois dernier': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')],
           'Année courante': [moment().startOf('year'), moment()],
           'Année dernière': [moment().subtract(1, 'year').startOf('year'), moment().subtract(1, 'year').endOf('year')],
        }
    }
    , cb);

    cb(start, end);

});
    const userCount = userIds.length;
    var dataColumns = [];
    for(var i=2; i<userCount + 3; i++)
        dataColumns.push(i);
    
	var dataTableConfig1 = {
		"paging": false,
		"lengthChange": false,
		"searching": true,
		"ordering": true,
		"info": false,
		"autoWidth": false,
		"orderCellsTop": true,
		"pageLength": 10000,
		"language": {
			"info":           "Page _PAGE_ / _PAGES_",
			"infoEmpty":      "Aucun résultat",
			"infoFiltered":   "(filtré sur _MAX_ lignes)",
			"infoPostFix":    "",
			"decimal": ".",
			"thousands": " ",
			"lengthMenu":     "Afficher _MENU_ lignes par page",
			"loadingRecords": "Chargement...",
			"processing":     "Traitement...",
			"search":         "Recherche:",
			"zeroRecords":    "Aucun résultat",
			"paginate": {
				"first":      "<<",
				"last":       ">>",
				"next":       ">",
				"previous":   "<"
			},
			"aria": {
				"sortAscending":  ": activer pour ordonner croissant",
				"sortDescending": ": activer pour ordonner décroissant"
			}
		},
		"data": tableData1,
		"columnDefs": [
			{
				"render": function ( data, type, row, meta ) {
					if(meta.col == userCount + 2) {
						return "<strong>"+displayWithUnit(unit, data)+"</strong>";
					}else{
						return displayWithUnit(unit, data);
					}

				},
				"targets": dataColumns
			}
			//{ "visible": false,  "targets": [ 3 ] }
		]
		,
		"footerCallback": function(tfoot, data, start, end, display) {
			const tableData = this.api().columns().data();
			var total = 0;
			var sum = new Array(userCount+2);
			for(var i=0; i<userCount + 2; i++) {
				sum[i] = 0;
			}
			$(this.api().column(i).footer()).html(
					'Total'
				)
			for(var i=2; i<userCount + 2; i++) {
				for(var j=0; j<tableData[i].length; j++) {
					sum[i] += parseFloat(tableData[i][j]);
					total += parseFloat(tableData[i][j]);
				}
			}
			for(var i=2; i<userCount + 2; i++) {
				$(this.api().column(i).footer()).html(
					displayWithUnit(unit, sum[i])
				)
			}
			$(this.api().column(userCount + 2).footer()).html(
				displayWithUnit(unit, total)
			)
		}
	};
	var dataTableConfig2 = $.extend(true, {}, dataTableConfig1);
	dataTableConfig2.data = tableData2;
	var dataTableConfig3 = $.extend(true, {}, dataTableConfig1);
	dataTableConfig3.data = tableData3;
	
    $('#reporting_table1').DataTable(dataTableConfig1);
    $('#reporting_table2').DataTable(dataTableConfig2);
    $('#reporting_table3').DataTable(dataTableConfig3);
}
</script>