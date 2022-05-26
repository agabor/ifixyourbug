<template>
  <div class="col-md-12 pe-2 mb-3">
    <label>Git repo url*</label>
    <input class="form-control" id="repo-url-input" placeholder="https://..." type="text" v-model="gitUrl" :disabled="!visible" @input="$emit('changeUrl', gitUrl)">
  </div>
  <label>Project sharing with*</label>
  <div class="col-md-12 d-flex pe-2">
    <div>
      <div class="form-check me-3" :class="{'mb-3': mode == undefined}">
        <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault1" :value="0" v-model="mode" :disabled="!visible" @change="$emit('changeAccessMode', mode)">
        <label class="form-check-label" for="flexRadioDefault1">
          Public repo
        </label>
      </div>
    </div>
    <div>
      <div class="form-check me-3">
        <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault2" :value="1" v-model="mode" :disabled="!visible" @change="$emit('changeAccessMode', mode)">
        <label class="form-check-label" for="flexRadioDefault2">
          Invite
        </label>
      </div>
    </div>
    <div>
      <div class="form-check me-3">
        <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault3" :value="2" v-model="mode" :disabled="!visible" @change="$emit('changeAccessMode', mode)">
        <label class="form-check-label" for="flexRadioDefault3">
          User account
        </label>
      </div>
    </div>
  </div>
  <div class="col-md-12 pe-2 mb-3" v-if="mode != undefined">
    <span v-if="mode == 0">Lorem ipsum dolor, sit amet consectetur adipisicing elit. Exercitationem, itaque sunt voluptatum unde repellendus nostrum distinctio eveniet quidem maxime animi repellat quasi quam officia provident possimus voluptate aliquam cum a. 0</span>
    <span v-if="mode== 1">Lorem ipsum dolor, sit amet consectetur adipisicing elit. Exercitationem, itaque sunt voluptatum unde repellendus nostrum distinctio eveniet quidem maxime animi repellat quasi quam officia provident possimus voluptate aliquam cum a. 1</span>
    <span v-if="mode == 2">Lorem ipsum dolor, sit amet consectetur adipisicing elit. Exercitationem, itaque sunt voluptatum unde repellendus nostrum distinctio eveniet quidem maxime animi repellat quasi quam officia provident possimus voluptate aliquam cum a. 2</span>
  </div>
</template>

<script>
import { ref, watch } from 'vue'
export default {
  name: 'ProjectSharing',
  emits:['changeUrl', 'changeAccessMode'],
  props: {
    visible: Boolean,
    url: String,
    accessMode: Number
  },
  setup(props) {
    const gitUrl = ref(props.url);
    const mode = ref(props.accessMode);

    watch(() => [props.visible, props.url, props.accessMode], () => {
      console.log(props.visible)
      gitUrl.value = props.url;
      mode.value = props.accessMode;
    })

    return { gitUrl, mode };
  }
}
</script>