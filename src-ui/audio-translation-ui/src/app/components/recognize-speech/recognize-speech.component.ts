import { Component } from '@angular/core';
import { AudioTranslationService } from '../../services/audio-translation.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-recognize-speech',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './recognize-speech.component.html',
  styleUrls: ['./recognize-speech.component.css'],
})
export class RecognizeSpeechComponent {
  recognizedText: string = '';
  errorMessage: string = '';

  constructor(private audioService: AudioTranslationService) { }

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.audioService.recognizeSpeech(file).subscribe({
        next: (response) => {
          this.recognizedText = response.text || 'No speech recognized';
          this.errorMessage = '';
        },
        error: (error) => {
          this.errorMessage = 'Error: ' + error.message;
          this.recognizedText = '';
        },
      });
    }
  }
}
