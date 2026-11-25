import { Component, input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  imports: [CommonModule],
  standalone: true,
  selector: 'app-kpi-card',
  styleUrl: './kpi-card.css',
  templateUrl: './kpi-card.html',
})
export class KpiCard {
  label = input.required<string>();
  value = input.required<string | number>()
  unit = input<string>('');
  subtext = input<string>('');
  variant = input<'default' | 'emerald'>('default');
}
