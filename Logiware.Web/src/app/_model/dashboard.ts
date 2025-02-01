
export interface Dashboard {
  totalInventory: number;
  totalReceived: number;
  totalOutbound: number;
  totalMissing: number;
  totalInbound: number;
}
export interface ShipmentByDate {
  createdAt: string;
  totalInbound: number;
  totalOutbound: number;
}
