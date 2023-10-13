import { Formik, Form, Field, FieldProps } from 'formik';
import { Button, Dropdown, DropdownProps, Header } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import { useStore } from '../../../app/stores/store';

interface Props {    
    activitiesOptions: string[]; 
  }

export default observer(function ActivityDropdownComponent ( props : Props) {
    const {activityStore : { updateWinningOption }} = useStore()


    const dropdownOptions = props.activitiesOptions.map((option) => ({
        key: option,
        value: option,
        text: option,
      }));
    
      const handleSubmit = (values: { selectedOption: string; }) => {
        console.log('1: ', values.selectedOption)
        updateWinningOption(values.selectedOption);
      };
    
      return (
        <Formik initialValues={{ selectedOption: '' }} onSubmit={handleSubmit}>
          {({ isSubmitting, isValid, setFieldValue }) => (
            <Form>
                <Header as='h2' content='Choose the team(option)' color='teal' textAlign="center" />
              <Field
                name="selectedOption"
                render={({ field }: FieldProps<string, DropdownProps>) => (
                  <Dropdown
                    {...field}
                    fluid
                    selection
                    options={dropdownOptions}
                    placeholder="Select an option"
                    onChange={(_e, data) => {
                      // Update the selected option in the form's state
                      setFieldValue('selectedOption', data.value);
                    }}
                  />
                )}
              />
              <Button
                disabled={!isValid || isSubmitting}
                loading={isSubmitting}
                positive
                content="Submit"
                type="submit"
                fluid
              />
            </Form>
          )}
        </Formik>
      );
})
