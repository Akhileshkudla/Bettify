import { useEffect, useState } from "react";
import { Button, Header, Input, Segment } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";
import { Link, useNavigate, useParams } from "react-router-dom";
import { ActivityFormValues } from "../../../app/models/activity";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { Formik, Form, FieldArray, Field, } from "formik";
import * as Yup from 'yup';
import MyTextInput from "./MyTextInputs";
import MyTextArea from "./MyTextArea";
import MySelectInput from "./MySelectInput";
import { categoryOptions } from "./options/categoryOptions";
import MyDateInput from "./MyDateInput";
import { v4 as uuid } from 'uuid';
import MyCheckBox from "./MyCheckBox";


export default observer(function AcitivityForm() {
    
    const { activityStore } = useStore()
    const { updateActivity, createActivity, loadActivity, loadingInitial } = activityStore;

    const { id } = useParams();
    const navigate = useNavigate()

    const [activity, setActivity] = useState<ActivityFormValues>(new ActivityFormValues());

    
    const validationSchema = Yup.object().shape({
        title: Yup.string().required('The activity title is required'),
        description: Yup.string().required('The activity description is required'),
        category: Yup.string().required(),
        date: Yup.string().required('Date is required').nullable(),
        city: Yup.string().required(),
        venue: Yup.string().required(),
        options: Yup.array()
            .of(Yup.string().required('Options are required'))
            .min(2, 'Atleast 2 options for betting should be specified').required()
    });

    useEffect(() => {
        if (id) loadActivity(id).then(activity => setActivity(new ActivityFormValues(activity)));
    }, [id, loadActivity])

    function HandleFormSubmit(activity: ActivityFormValues) {
        if (!activity.id) {
            let newActivity = {
                ...activity,
                id: uuid()
            };
            createActivity(newActivity).then(() => navigate(`/activities/${newActivity.id}`));
        }
        else {
            updateActivity(activity).then(() => navigate(`/activities/${activity.id}`));
        }
    }

    if (loadingInitial) return <LoadingComponent content="Loading..." />

    return (
        <Segment clearing>
            <Header content='Activity Details' sub color="teal" />
            <Formik validationSchema={validationSchema} enableReinitialize initialValues={activity} onSubmit={values => HandleFormSubmit(values)}>
                {({ handleSubmit, isValid, isSubmitting, dirty }) => (
                    <Form className="ui form" onSubmit={handleSubmit} autoComplete='off'>
                        <MyTextInput name='title' placeholder="Title" />
                        <MyTextArea rows={3} placeholder='Description' name='description' />
                        <MySelectInput options={categoryOptions} placeholder='Category' name='category' />
                        <MyDateInput
                            placeholderText='Date'
                            name='date'
                            showTimeSelect
                            timeCaption="time"
                            dateFormat='MMMM d, yyyy h:mm aa'
                        />
                        <Header content='Location Details' sub color="teal" />
                        <MyTextInput placeholder='City' name='city' />
                        <MyTextInput placeholder='Venue' name='venue' />
                        <MyTextInput label="Bet win amount" placeholder='Bet win amount' name='amountifwon' />
                        <MyTextInput label="Bet lose amount" placeholder='Bet lose amount' name='amountiflose' />
                        <MyCheckBox label="Is Mandatory?" placeholder="Is Mandatory" name='ismandatoryactivity' />
                        <Header content='Options' sub color="teal" />
                        <FieldArray name="options">
                            {(arrayHelpers) => (
                                <div>
                                    {arrayHelpers.form.values.options.map((_option : undefined, index : number) => (
                                        <div key={index} style={{ marginBottom: '10px' }}>
                                            <Field
                                                name={`options.${index}`}
                                                placeholder="Enter Options"
                                                as={Input}
                                            />
                                            <Button
                                                type="button"
                                                onClick={() => arrayHelpers.remove(index)}
                                            >
                                                Remove
                                            </Button>
                                        </div>
                                    ))}
                                    <Button type="button" onClick={() => arrayHelpers.push('')}>
                                        Add Options
                                    </Button>
                                </div>
                            )}
                        </FieldArray>
                        <Button
                            disabled={isSubmitting || !dirty || !isValid}
                            loading={isSubmitting} floated="right" positive type="submit" content='Submit' />
                        <Button as={Link} to='/activities' floated="right" type="button" content='Cancel' />
                    </Form>
                )}
            </Formik>
        </Segment>
    )
})