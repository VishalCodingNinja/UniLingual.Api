import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AudioTranslationService {
  private baseUrl = 'https://localhost:44314/api/AudioTranslation'; // Replace with your API's base URL

  constructor(private http: HttpClient) { }

  // 1. Speech to Text
  speechToText(audioFile: File): Observable<any> {
    const formData = new FormData();
    formData.append('audioFile', audioFile);
    return this.http.post(`${this.baseUrl}/speech-to-text`, formData);
  }

  // 2. Send to Teams
  sendToTeams(teamId: string, channelId: string, message: string): Observable<any> {
    const body = { TeamId: teamId, ChannelId: channelId, Message: message };
    return this.http.post(`${this.baseUrl}/send-to-teams`, body);
  }

  // 3. Text to Speech
  textToSpeech(text: string): Observable<Blob> {
    const formData = new FormData();
    formData.append('text', text);

    return this.http.post(`${this.baseUrl}/text-to-speech`, formData, {
      responseType: 'blob', // Expect audio as a binary file
    });
  }

  // Method to convert text to speech using the recorded voice
  customTextToSpeech(voiceSample: Blob, text: string): Observable<Blob> {
    const formData = new FormData();
    formData.append('voiceSample', voiceSample, 'voiceSample.wav');
    formData.append('text', text);

    return this.http.post(`${this.baseUrl}/custom-text-to-speech`, formData, {
      responseType: 'blob',
    });
  }



  // 4. Recognize Speech
  recognizeSpeech(audioFile: File): Observable<any> {
    const formData = new FormData();
    formData.append('audio', audioFile);
    return this.http.post(`${this.baseUrl}/recognize`, formData);
  }

  // 5. Translate Text
  translateText(text: string, toLanguage: string): Observable<any> {
    const params = { text, toLanguage };
    return this.http.post(`${this.baseUrl}/translate`, null, { params });
  }
}
