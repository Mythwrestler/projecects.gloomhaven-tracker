<script lang="ts">
  import {
    TextField,
    CheckMarkIcon,
    DropDown,
    Dialog,
    DialogHeader,
    DialogBody,
    DialogFooter,
    RadioGroup,
  } from "../../common/Components";

  let dialogIsOpen = false;
  const openDialog = () => {
    dialogIsOpen = true;
  };
  const closeDialog = () => {
    dialogIsOpen = false;
  };
</script>

<div>
  <div class="p-1">
    <TextField value="" placeholderText="square" buttonLabel={CheckMarkIcon} />
  </div>
  <div class="p-1">
    <TextField
      value=""
      placeholderText="rounded"
      variant="rounded"
      buttonLabel={CheckMarkIcon}
    />
  </div>
  <div class="p-1"><TextField value="" placeholderText="No Icon" /></div>
  <div class="p-1">
    <TextField
      value=""
      border
      placeholderText="Icon w/ border"
      buttonLabel={CheckMarkIcon}
    />
  </div>
  <div class="p-1">
    <TextField value="" border placeholderText="No Icon w/ border" />
  </div>
  <div class="p-1">
    <DropDown
      placeHolder="Scenario"
      options={[
        { label: "Test Scenario 01", value: "testScenario01" },
        { label: "Test Scenario 02", value: "testScenario02" },
        { label: "Test Scenario 03", value: "testScenario03" },
      ]}
    />
  </div>
  <div class="p-1">
    <DropDown
      placeHolder="Scenario 2"
      variant="rounded"
      options={[
        { label: "Test Scenario 01", value: "testScenario01" },
        { label: "Test Scenario 02", value: "testScenario02" },
        { label: "Test Scenario 03", value: "testScenario03" },
      ]}
    />
  </div>
  <div class="p-1 flex h-12">
    <button class="mx-auto" on:click={openDialog}>Open Dialog</button>
  </div>
  <div class="p-1">
    <RadioGroup
      value={2}
      centered
      options={[
        { label: "Test Value 01", value: 1 },
        { label: "Test Value 02", value: 2 },
        { label: "Test Value 03", value: 3 },
        { label: "Test Value 04", value: 4 },
      ]}
    />
  </div>
  <Dialog open={dialogIsOpen} onClose={closeDialog}>
    <DialogHeader slot="DialogHeader">Header</DialogHeader>
    <DialogBody slot="DialogBody">Body</DialogBody>
    <DialogFooter slot="DialogFooter">Footer</DialogFooter>
  </Dialog>
</div>
