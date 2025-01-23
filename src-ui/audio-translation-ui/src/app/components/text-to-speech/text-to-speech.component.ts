import { Component } from '@angular/core';
import { AudioTranslationService } from '../../services/audio-translation.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-text-to-speech',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './text-to-speech.component.html',
  styleUrls: ['./text-to-speech.component.css'],
})
export class TextToSpeechComponent {
  text: string = '';
  audioUrl: string | null = null;
  errorMessage: string = '';

  constructor(private audioService: AudioTranslationService) { }

  convertTextToSpeech(): void {
    this.audioService.textToSpeech(this.text).subscribe({
      next: (audioBlob) => {
        this.audioUrl = URL.createObjectURL(audioBlob);
        this.errorMessage = '';
      },
      error: (error) => {
        this.errorMessage = 'Error: ' + error.message;
        this.audioUrl = null;
      },
    });
  }
}
