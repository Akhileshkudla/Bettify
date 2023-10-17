import { Field, Form, Formik, FieldProps } from "formik";
import { observer } from "mobx-react-lite";
import { Button, Header } from "semantic-ui-react";
import { Activity } from "../../../app/models/activity";
import { useState, useEffect } from "react";
import { useStore } from "../../../app/stores/store";

interface Props {
  activity: Activity;
}

export default observer(function ActivityDetailsPlaceBet({ activity }: Props) {
  const { activityStore: { updateAttendance } } = useStore();
  const [selectedOption, setSelectedOption] = useState<string>(activity.options[0]);

  const handleButtonClick = (option: string) => {
    setSelectedOption(option);
    console.log('Selected option:', option);
  };

  useEffect(() => {
    setSelectedOption(activity.options[0]);
  }, [activity.options]);

  return (
    <Formik
      initialValues={{ selectedOption }}
      onSubmit={(values) => {
        console.log('Selected option:', values.selectedOption);
        updateAttendance();
        console.log('Form submitted with selected option:', values.selectedOption);
      }}
    >
      {({ handleSubmit, isSubmitting, isValid, setFieldValue }) => (
        <Form className='ui form error' onSubmit={handleSubmit} autoComplete="off">
          <Header as='h2' content='Choose any option' color='teal' textAlign="center" />
          <div style={{ marginBottom: '10px' }}>
            {activity.options.map((option, index) => (
              <Field key={index}>
                {({ }: FieldProps) => (
                  <Button
                    type="button"
                    content={option}
                    color={selectedOption === option ? 'instagram' : undefined}
                    onClick={() => {
                      handleButtonClick(option);
                      setFieldValue('selectedOption', option);
                    }}
                  />
                )}
              </Field>
            ))}
          </div>
          <Button
            disabled={!isValid || isSubmitting}
            loading={isSubmitting}
            positive
            content='Place bet'
            type="submit"
            fluid
          />
        </Form>
      )}
    </Formik>
  )
})
