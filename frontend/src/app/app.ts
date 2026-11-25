import { Component, OnInit, computed, inject, signal } from '@angular/core';
import {CommonModule} from '@angular/common';
import {AssetService} from './features/assets/services/asset.service';
import {Asset, PortfolioKpis} from './features/assets/models/asset.model';
import { KpiCard } from './features/assets/components/kpi-card/kpi-card';

@Component({
  imports: [CommonModule, KpiCard],
  selector: 'app-root',
  standalone: true,
  styleUrl: './app.css',
  templateUrl: './app.html',
})
export class App {
  private readonly assetService = inject(AssetService);

  assets = signal<Asset[]>([]);
  isLoading = signal<boolean>(true);
  errorMessage = signal<string | null>(null);

  kpis = computed<PortfolioKpis>(()=>{
    const list = this.assets();
    const totalAssets = list.length;
    const totalCapacityMw = list.reduce((sum, asset)=> sum + asset.installedCapacity,0);
    const totalOutputMw = list.reduce((sum,asset)=> sum + asset.currentOutput,0);
    const operationalCount = list.filter(asset => asset.status === 'Operational').length;
   
      return {
        totalAssets, 
        totalCapacityMW: Number(totalCapacityMw.toFixed(2)),
        totalOutputMW: Number(totalOutputMw.toFixed(2)),
        operationalCount
      };
    }
  );

  ngOnInit() :void{
    this.loadAssets();
  }

  loadAssets(): void {
    this.isLoading.set(true);
    this.errorMessage.set(null);
    
    this.assetService.getAssets().subscribe({
      next:(data)=>{
        this.assets.set(data);
        this.isLoading.set(false);
      },
      error:(err)=>{
        console.error('Error loading assets:', err);
        this.errorMessage.set('Failed loading assets. Please try again later.');
        this.isLoading.set(false);
      }
    });
  }

}
