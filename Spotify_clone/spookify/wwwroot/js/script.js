

can.Component.extend({
  tag: 'audio-player',
  view: `
  
  <input type="range" value="0" max="1" step="any" class="slider" id="myslider" value:bind="percentComplete"/>
      
      <div id="buttons">
      <button on:click="togglePlay()" class="btn btn-custom">
        {{#if(playing)}}<span title="Stop" id="stop" class="glyphicon glyphicon-stop aligned"><i class="material-icons">stop</i></span>{{else}}<span title="Play" id="play" class="glyphicon icon-play aligned"><i class="material-icons">play_circle_outline</i></span>{{/if}}
      </button>
      <button on:click="Restart()" class="btn btn-custom"><span title="Restart" id="restart" class="glyphicon glyphicon-step-backward aligned"><i class="material-icons">skip_previous</i></span></button>
      <span id= "playertime" class="text-center">{{formatTime(currentTime)}} / {{formatTime(duration)}} </span>
      <button on:click="Backward5()" class="btn btn-custom"><span title="-5 seconds" id="backward" class="glyphicon glyphicon-fast-backward aligned"><i class="material-icons">fast_rewind</i></span></button>
      <button on:click="Forward5()" class="btn btn-custom"><span title="+5 seconds" id="forward" class="glyphicon glyphicon-fast-forward aligned"><i class="material-icons">fast_forward</i></span></button>
      <span class="separator"></span>
      <button on:click="volumeUpdate('down')" class="btn btn-custom"><span title="Volume Down" id="volumedown" class="glyphicon glyphicon-minus aligned"><i class="material-icons">volume_down</i></span></button>
      <input type="range" value="0" max="1" step="any" class="volslider btn" id="volslider" value:bind="volume"/>
      <button on:click="volumeUpdate('up')" class="btn btn-custom"><span title="Volume Up" id="volumeup" class="glyphicon glyphicon-plus aligned"><i class="material-icons">volume_up</i></span></button>
      </div>
    <audio controls id="player"
    on:play="play()"
    on:pause="pause()"
    on:timeupdate="updateTimes(scope.element)"
    on:loadedmetadata="updateTimes(scope.element)">
    <source src="{{src}}"/>
    </audio>
  `,
  ViewModel: {
    src: 'https://upload.wikimedia.org/wikipedia/commons/d/d8/Johannes_Brahms_-_Ungarischer_Tanz_5_g-moll.ogg',
    playing: "boolean",
    duration: "number",
    currentTime: "number",
    volume: "number",

    get percentComplete() {
      return this.currentTime / this.duration;
    },
    set percentComplete(newVal) {
      this.currentTime = newVal * this.duration;
    },
    get vol() {
      return this.volume;
    },
    set vol(newVal) {
      this.volume = newVal * this.volume;
    },

    updateTimes(audioElement) {
      this.currentTime = audioElement.currentTime || 0;
      this.duration = audioElement.duration;
      this.volume = audioElement.volume || 0;
    },
    formatTime(time) {
      if (time === null || time === undefined) {
        return "--"
      }
      const durmins = Math.floor(time / 60);
      let dursecs = Math.floor(time - durmins * 60);
      if (dursecs < 10) {
        dursecs = "0" + dursecs;
      }
      return durmins + ":" + dursecs
    },

    play() {
      this.playing = true;
    },
    pause() {
      this.playing = false;
    },
    togglePlay() {
      this.playing = !this.playing;
    },
    Restart(){
      var audio = document.getElementById('player');
      audio.currentTime = 0;
    },
    Backward5(){
      var audio = document.getElementById('player');
      if(this.currentTime > 5){
        audio.currentTime = audio.currentTime - 5;
      }
      else{
        this.Restart();
      }
    },
    Forward5(){
      var audio = document.getElementById('player');
      if(this.duration-this.currentTime > 5){
        audio.currentTime = audio.currentTime + 5;
      }
      else{
        this.Restart();
      }
    },
    volumeUpdate(change){
      var audio = document.getElementById('player');
      if(audio.volume > 0.05 && change =="down"){
        console.log(audio.volume)
        audio.volume = (audio.volume*100 - 5)/100;
      }
      else if(audio.volume < 0.95 && change =="up"){
        console.log(audio.volume)
        audio.volume = (audio.volume*100 + 5)/100;
      }
    },

    connectedCallback(element) {
      this.listenTo("playing", function (ev, isPlaying) {
        if (isPlaying) {
          element.querySelector("audio").play();
        } else {
          element.querySelector("audio").pause();
        }
      });
      // this.listenTo("currentTime", function(ev, currentTime){
      //   element.querySelector("audio").currentTime = currentTime;
      // });
    }
  }
})
