import { Component } from '@angular/core';
import { AudioTranslationService } from '../../services/audio-translation.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-send-to-teams',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './send-to-teams.component.html',
  styleUrls: ['./send-to-teams.component.css'],
})
export class SendToTeamsComponent {
  teamId: string = '';
  channelId: string = '';
  message: string = '';
  responseMessage: string = '';
  errorMessage: string = '';

  constructor(private audioService: AudioTranslationService) { }

  sendMessage(): void {
    this.audioService.sendToTeams(this.teamId, this.channelId, this.message).subscribe({
      next: (response) => {
        this.responseMessage = 'Message sent successfully!';
        this.errorMessage = '';
      },
      error: (error) => {
        this.errorMessage = 'Error: ' + error.message;
        this.responseMessage = '';
      },
    });
  }
}
