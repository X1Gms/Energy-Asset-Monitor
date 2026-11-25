export type AssetType = 'Solar' | 'Wind' | 'Battery';
export type AssetStatus = 'Operational' | 'Warning' | 'Offline';

export interface Asset {
    id: number;
    name : string;
    type: AssetType;
    location: string;
    installedCapacity: number; // in MW
    currentOutput: number; // in MW
    status: AssetStatus;
    lastUpdate: string; // ISO 8601 date string
}

export interface PortfolioKpis {
    totalAssets: number;
    totalCapacityMW: number;
    totalOutputMW: number;
    operationalCount: number;
}