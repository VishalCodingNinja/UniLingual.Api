import { Component } from '@angular/core';
import { AudioTranslationService } from '../../services/audio-translation.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-speech-to-text',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './speech-to-text.component.html',
  styleUrls: ['./speech-to-text.component.css'],
})
export class SpeechToTextComponent {
  transcript: string = '';
  errorMessage: string = '';

  constructor(private audioService: AudioTranslationService) { }

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.audioService.speechToText(file).subscribe({
        next: (response) => {
          this.transcript = response.text || 'No transcript available';
        },
        error: (error) => {
          this.errorMessage = 'Error: ' + error.message;
        },
      });
    }
  }
}
