import { Component, ChangeDetectorRef, AfterViewChecked } from '@angular/core';
import { AudioTranslationService } from '../../services/audio-translation.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-custom-text-to-speech',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './custom-text-to-speech.component.html',
  styleUrls: ['./custom-text-to-speech.component.css'],
})
export class CustomTextToSpeechComponent implements AfterViewChecked {
  text: string = '';
  audioUrl: string | null = null;
  errorMessage: string = '';
  isRecording: boolean = false;
  recordedAudio: Blob | null = null;
  recordedTime: number = 0;
  mediaRecorder: MediaRecorder | null = null;
  recordingInterval: any;
  isLoading: boolean = false; // For loading indicator

  constructor(private audioService: AudioTranslationService, private cdRef: ChangeDetectorRef) { }

  ngAfterViewChecked(): void {
    if (this.recordedAudio) {
      this.cdRef.detectChanges();
    }
  }

  startRecording(): void {
    if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
      navigator.mediaDevices.getUserMedia({ audio: true }).then((stream) => {
        this.isRecording = true;
        this.recordedTime = 0;
        this.recordedAudio = null;
        this.mediaRecorder = new MediaRecorder(stream);

        this.mediaRecorder.ondataavailable = (event) => {
          if (event.data.size > 0) {
            this.recordedAudio = event.data;
            this.cdRef.detectChanges();
          }
        };

        this.mediaRecorder.start();
        this.recordingInterval = setInterval(() => {
          this.recordedTime += 1;
        }, 1000);
      }).catch((err) => {
        this.errorMessage = 'Error accessing the microphone: ' + err.message;
      });
    } else {
      this.errorMessage = 'Your browser does not support audio recording.';
    }
  }

  stopRecording(): void {
    if (this.mediaRecorder) {
      this.mediaRecorder.stop();
      this.isRecording = false;
      clearInterval(this.recordingInterval);
    }
  }

  convertTextToSpeech(): void {
    if (this.recordedAudio && this.text) {
      this.isLoading = true; // Show loading indicator
      this.audioService.customTextToSpeech(this.recordedAudio, this.text).subscribe({
        next: (audioBlob) => {
          this.audioUrl = URL.createObjectURL(audioBlob);
          this.errorMessage = '';
          this.isLoading = false; // Hide loading indicator
        },
        error: (error) => {
          this.errorMessage = 'Error: ' + error.message;
          this.audioUrl = null;
          this.isLoading = false; // Hide loading indicator
        },
      });
    } else {
      this.errorMessage = 'Please record your voice and provide text.';
    }
  }
}
