import { Component, inject, OnInit, signal } from '@angular/core';
import {BaseChartDirective} from "ng2-charts";
import {ChartConfiguration} from "chart.js";
import { SiteService } from '../_services/site.service';
import { Dashboard, ShipmentByDate } from '../_model/dashboard';
import { ShipmentService } from '../_services/shipment.service';
import { DatePipe } from '@angular/common';
import { NotificationService } from '../_services/notification.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [BaseChartDirective],
  providers: [DatePipe],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {


  siteSerivce = inject(SiteService)
  shipmentService = inject(ShipmentService)
  datePipe = inject(DatePipe)

  protected readonly _dashboard = signal<Dashboard | null>(null)
  protected readonly _shipmentComparision = signal<ShipmentByDate[] | null>(null)


  lineChartData: ChartConfiguration['data'] = {datasets: [], labels: [] }
  ngOnInit(): void {

    this.siteSerivce.getDashboard().subscribe(dashboard => this._dashboard.set(dashboard));
  this.shipmentService.getShipmentComparison().subscribe(response => {
  // Extract data from the response for the chart
  const inboundData = response.map(shipment => shipment.totalInbound);
  const outboundData = response.map(shipment => shipment.totalOutbound);
  const labels = response.map(shipment => shipment.createdAt);

  this.lineChartData = {
    datasets: [
      {
        data: inboundData,
        label: 'In-Bounds',
        fill: true,
        tension: 0.4,
        backgroundColor: (context: any) => {
          const chart = context.chart;
          const canvas = chart.canvas;
          const ctx = canvas.getContext("2d");
          const gradient = ctx.createLinearGradient(0, 0, 0, canvas.height);
          gradient.addColorStop(0, "rgba(255, 87, 51, 0.5)");
          gradient.addColorStop(1, "rgba(255, 255, 255, 0)");
          return gradient;
        },
      },
      {
        data: outboundData,
        label: 'Out-Bounds',
        fill: true,
        borderColor: "rgba(0, 0, 0, 0)",
        tension: 0.4,
        backgroundColor: (context: any) => {
          const chart = context.chart;
          const canvas = chart.canvas;
          const ctx = canvas.getContext("2d");
          const gradient = ctx.createLinearGradient(0, 0, 0, canvas.height);
          gradient.addColorStop(0, "rgba(53, 162, 235, 0.5)");
          gradient.addColorStop(1, "rgba(255, 255, 255, 0)");
          return gradient;
        },
      },
    ],
    labels: labels,
  };
});

  }


   // @ts-ignore
    lineChartOptions: ChartConfiguration['options'] = {
      responsive: true,

      plugins: {
        legend: {

          position: "top",
        },
        title: {
          display: false,

        },

      },
      scales: {
        x: {
          display: true,
          grid: {
            display: false,
          },
        },
        y: {
          beginAtZero: true,
          display: false,
        },
      },
    };
}
