import { Component } from '@angular/core';
import { AudioTranslationService } from '../../services/audio-translation.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-translate-text',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './translate-text.component.html',
  styleUrls: ['./translate-text.component.css'],
})
export class TranslateTextComponent {
  text: string = '';
  toLanguage: string = '';
  translatedText: string = '';
  errorMessage: string = '';

  constructor(private audioService: AudioTranslationService) { }

  translate(): void {
    this.audioService.translateText(this.text, this.toLanguage).subscribe({
      next: (response) => {
        this.translatedText = response[0]?.translations[0]?.text || 'Translation unavailable';
        this.errorMessage = '';
      },
      error: (error) => {
        this.errorMessage = 'Error: ' + error.message;
        this.translatedText = '';
      },
    });
  }
}
