<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="WebForm5.aspx.cs" Inherits="BomChange.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script src="Charts/Chart.budle.js"></script>
    <script src="Charts/utils.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyContent" runat="server">

<div id="container" style="width: 75%;">
		<canvas id="canvas"></canvas>
	</div>



	<script>
        var MONTHS = []
        var NUM = []
        // alert(' GetDatas() ')
        $.ajax(
            {
                url: 'Handler2.ashx',
                type: 'post',
                dataType: 'json',
                error: function (data) { alert('失败'); },
                success: function (data) {
                    //alert(data);
                    //alert(data.length)
                    $.each(data, function (k, v) {
                        //alert(v.Product_Name)
                        MONTHS[k] = v.Product_Name
                        NUM[k] = v.id;
                    })
                    // alert(MONTHS)
                    getvalue(MONTHS, NUM)

                }

            }
        )

        function getvalue(arr, numY) {
            //var MONTHS = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
            var color = Chart.helpers.color;
            var barChartData = {
                labels: arr,
                datasets: [{
                    label: '物料数量',
                    backgroundColor: color(window.chartColors.red).alpha(0.5).rgbString(),
                    borderColor: window.chartColors.red,
                    borderWidth: 1,
                    data: numY,
                }]

            }

              // Define a plugin to provide data labels
  Chart.plugins.register({
   afterDatasetsDraw: function(chart) {
    var ctx = chart.ctx;

    chart.data.datasets.forEach(function(dataset, i) {
     var meta = chart.getDatasetMeta(i);
     if (!meta.hidden) {
      meta.data.forEach(function(element, index) {
       // Draw the text in black, with the specified font
       ctx.fillStyle = 'rgb(56, 99, 200)';

       var fontSize = 16;
       var fontStyle = 'normal';
       var fontFamily = 'BankGothic Lt BT';
       ctx.font = Chart.helpers.fontString(fontSize, fontStyle, fontFamily);

       // Just naively convert to string for now
       var dataString = dataset.data[index].toString();

       // Make sure alignment settings are correct
       ctx.textAlign = 'center';
       ctx.textBaseline = 'middle';

       var padding = 5;
       var position = element.tooltipPosition();
       ctx.fillText(dataString, position.x, position.y - (fontSize / 2) - padding);
      });
     }
    });
   }
  });

			var ctx = document.getElementById('canvas').getContext('2d');
			window.myBar = new Chart(ctx, {
				type: 'bar',
				data: barChartData,
				options: {
					responsive: true,
					legend: {
						position: 'top',
					},
                    title: {
                        
						display: true,
                        text: '人员物料更新统计'
                        
					}
				}
			});

        }
      


	</script>



</asp:Content>
